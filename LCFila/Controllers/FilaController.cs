using AutoMapper;
using LCFila.Extensions;
using LCFila.ViewModels;
using LCFilaApplication.Enums;
using LCFilaApplication.Interfaces;
using LCFilaApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LCFila.Controllers
{
    [Authorize(Roles = "SysAdmin,EmpAdmin,OperatorEmp")]
    public class FilaController : BaseController
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly IFilaRepository _filaRepository;
        private readonly IFilaPessoaRepository _filapessoaRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        public FilaController(INotificador notificador,
                              IMapper mapper,
                              UserManager<AppUser> userManager,
                              IPessoaRepository pessoaRepository,
                              IFilaPessoaRepository filapessoaRepository,
                              IFilaRepository filaRepository,
                              IEmpresaLoginRepository empresaRepository) : base(notificador, userManager, empresaRepository)
        {
            _pessoaRepository = pessoaRepository;
            _filaRepository = filaRepository;
            _filapessoaRepository = filapessoaRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        // GET: FilaController
        public async Task<IActionResult> Index(/*Guid? id*/)
        {
            ConfigEmpresa();
            //var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var user = await _userManager.Users.Include(p => p.empresaLogin).SingleOrDefaultAsync(p => p.UserName == User.Identity.Name);
            var allusers = _userManager.Users.Include(p => p.empresaLogin).Where(p => p.empresaLogin.Id == user.empresaLogin.Id).ToList();  
            var Empresaid = user.empresaLogin.Id;
            var pegarfila = await _filaRepository.ObterTodos();
            List<Fila> filasdousuario = new List<Fila>();
            if (User.IsInRole("EmpAdmin"))
            {
                filasdousuario = pegarfila.Where(p => p.EmpresaId == Empresaid).ToList();
            } else
            {
                filasdousuario = pegarfila.Where(p => p.UserId == Guid.Parse(user.Id)).ToList();
            }
            
            var pessoas = await _pessoaRepository.ObterTodos();
            var teste = _mapper.Map<IEnumerable<FilaViewModel>>(filasdousuario);

            foreach (var item in teste)
            {
                item.NomeUser = allusers.SingleOrDefault(p => p.Id == item.UserId.ToString()).UserName;
            }
            //var teste1 = _mapper.Map<IEnumerable<PessoaViewModel>>(pessoas);
            //if (id == null)
            //{
            //    var filahoje = pegarfila.SingleOrDefault(p => p.DataInicio.Date == DateTime.Today && p.EmpresaId == Empresaid);
            //    if(filahoje == null)
            //    {
            //        FilaViewModel novafila = new FilaViewModel();
            //        novafila.DataInicio = DateTime.Now;
            //        novafila.TempoMedio = "30";
            //        novafila.EmpresaId = Empresaid;
            //        var fila = _mapper.Map<Fila>(novafila);
            //        await _filaRepository.Adicionar(fila);
            //        List<PessoaViewModel> newpessoas = new List<PessoaViewModel>();
            //        ViewBag.idFila = fila.Id;
            //        ViewBag.info = "Fila nova criada!";
            //        return View(newpessoas);
            //    } 
            //    else {
            //        //var pessoas = await _pessoaRepository.ObterTodos();
            //        var pessoasdafila = pessoas.Where(p => p.FilaId == filahoje.Id);
            //        var pessoasnafila = _mapper.Map<IEnumerable<PessoaViewModel>>(pessoasdafila);
            //        ViewBag.idFila = filahoje.Id;
            //        //ViewBag.info = "Fila já existente!";
            //        return View(pessoasnafila);
            //    }
            //} 
            //else
            //{
            //    var filadoid = pegarfila.SingleOrDefault(p => p.Id == id && p.EmpresaId == Empresaid);
            //    if(filadoid.DataInicio.Date == DateTime.Today)
            //    {
            //       // var pessoas = await _pessoaRepository.ObterTodos();
            //        var pessoasdafila = pessoas.Where(p => p.FilaId == filadoid.Id);
            //        var pessoasnafila = _mapper.Map<IEnumerable<PessoaViewModel>>(pessoasdafila);
            //        ViewBag.info = "Fila já existente!";
            //        ViewBag.idFila = filadoid.Id;
            //        return View(pessoasnafila);
            //    } else
            //    {
            //        FilaViewModel novafila = new FilaViewModel();
            //        novafila.DataInicio = DateTime.Now;
            //        novafila.TempoMedio = "30";
            //        novafila.EmpresaId = Empresaid;
            //        var fila = _mapper.Map<Fila>(novafila);
            //        await _filaRepository.Adicionar(fila);
            //        List<PessoaViewModel> newpessoas = new List<PessoaViewModel>();
            //        ViewBag.idFila = fila.Id;
            //        ViewBag.info = "Fila nova criada!";
            //        return View(newpessoas);
            //    }
            //}

            //var pessoas = await _pessoaRepository.ObterTodos();
            
                    
            return View(teste.ToList());
        }

        // GET: FilaController/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            ConfigEmpresa();
            var user = await _userManager.Users.Include(p => p.empresaLogin).SingleOrDefaultAsync(p => p.UserName == User.Identity.Name);
            var Empresaid = user.empresaLogin.Id;
            var filatoopen = await _filaRepository.ObterPorId(id);
            //var allusers = _userManager.Users.Include(p => p.empresaLogin).Where(p => p.empresaLogin.Id == user.empresaLogin.Id).ToList();
            var pessoas = await _pessoaRepository.Buscar(p => p.FilaId == id);
            var pessoasdafila = _mapper.Map<IEnumerable<PessoaViewModel>>(pessoas);
            ViewBag.idFila = id;
            ViewBag.statusfila = filatoopen.Status.ToString();
            return View(pessoasdafila);
        }

        public async Task<IActionResult> ReAbrir(Guid id)
        {
            ConfigEmpresa();
            var filatoopen = await _filaRepository.ObterPorId(id);

            filatoopen.Status = FilaStatus.Aberta;
            await _filaRepository.Atualizar(filatoopen);
            await _filaRepository.SaveChanges();
            //var filatoopenviewmodel = _mapper.Map<FilaViewModel>(filatoopen);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Finalizar(Guid id)
        {
            ConfigEmpresa();
            var filatoopen = await _filaRepository.ObterPorId(id);

            filatoopen.Status = FilaStatus.Finalizada;
            await _filaRepository.Atualizar(filatoopen);
            await _filaRepository.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> CreateFila()
        {
            ConfigEmpresa();
            var user = await _userManager.Users.Include(p => p.empresaLogin).SingleOrDefaultAsync(p => p.UserName == User.Identity.Name);
            var Empresaid = user.empresaLogin.Id;

            FilaViewModel filamodel = new FilaViewModel
            {
                UserId = Guid.Parse(user.Id),
                EmpresaId = Empresaid,
                TempoMedio = "30"
            };
            return View(filamodel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFila(FilaViewModel filamodel)
        {
            ConfigEmpresa();
            filamodel.DataInicio = DateTime.Now;
            filamodel.Ativo = true;
            filamodel.Status = FilaStatus.Aberta; //(FilaStatus)Enum.Parse(typeof(FilaStatus), "Aberta")
            var fila = _mapper.Map<Fila>(filamodel);
            await _filaRepository.Adicionar(fila);
            return RedirectToAction("Index");
        }
        // GET: FilaController/Create
        //[ClaimsAuthorize("Funcionário", "Criar")]
        [HttpGet]
        public async Task<IActionResult> Create(Guid id)
        {
            ConfigEmpresa();
            // var user = await _userManager.Users.Include(p => p.empresaLogin).SingleOrDefaultAsync(p => p.UserName == User.Identity.Name);
            //var Empresaid = user.empresaLogin.Id;
            AdicionarpessoasViewModel pessoasviewmodel = new AdicionarpessoasViewModel();
            pessoasviewmodel.filaId = id;
            return View(pessoasviewmodel);
        }

        public async Task<IActionResult> IniciarFila()
        {
            ConfigEmpresa();
            // Checkar se já existe fila de hoje
            //var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var user = await _userManager.Users.Include(p => p.empresaLogin).SingleOrDefaultAsync(p => p.UserName == User.Identity.Name);
            var Empresaid = user.empresaLogin.Id;
            //var pegarfila = await _filaRepository.ObterTodos();
            //var filahoje = pegarfila.SingleOrDefault(p => p.DataInicio.Date == DateTime.Today);
            //if(filahoje == null)
            //{
                FilaViewModel novafila = new FilaViewModel();
                novafila.DataInicio = DateTime.Now;
                novafila.TempoMedio = "30";
                novafila.EmpresaId = Empresaid;
                novafila.UserId = Guid.Parse(user.Id);
                var fila = _mapper.Map<Fila>(novafila);
                await _filaRepository.Adicionar(fila);
                //ViewBag.info = "Fila nova criada!";
                return RedirectToAction("Index", fila.Id);
            //} else {
            //    //ViewBag.info = "Fila já existente!";
            //    return RedirectToAction("Index", filahoje.Id);
            //}
           
            //return View();
        }

        // POST: FilaController/Create
        [HttpPost]
        public async Task<IActionResult> Create(AdicionarpessoasViewModel pessoaViewModel)
        {
            ConfigEmpresa();
            if (!ModelState.IsValid) return View(pessoaViewModel);

            //FilaViewModel newfila = new FilaViewModel()
            //{
            //    TempoMedio = "30",
            //    DataInicio = DateTime.Now
            //};
            pessoaViewModel.pessoa.DataEntradaNaFila = DateTime.Now;
            pessoaViewModel.pessoa.Ativo = true;
            pessoaViewModel.pessoa.Status = PessoaStatus.Esperando;
            //var pegarfila = await _filaRepository.ObterTodos();
            //var filahoje = pegarfila.SingleOrDefault(p => p.Id == pessoaViewModel.filaId);
            var pessoas = await _pessoaRepository.ObterTodos();
            //var filahoje = pegarfila.SingleOrDefault(p => p.Id == pessoaViewModel.filaId);
            var pessoasdafila = pessoas.Where(p => p.FilaId == pessoaViewModel.filaId && p.Ativo == true && p.Status == PessoaStatus.Esperando).ToList();

            var pessoa = _mapper.Map<Pessoa>(pessoaViewModel.pessoa);

            //pessoaViewModel.DataEntradaNaFila = newfila.DataInicio;
            //var pessoas1 = await _pessoaRepository.ObterTodos();
            if (pessoaViewModel.pessoa.Preferencial)
            {
                pessoa.Posicao = 1;
                foreach (var item in pessoas.Where(p => p.FilaId == pessoaViewModel.filaId && p.Ativo == true && p.Status == PessoaStatus.Esperando).OrderBy(p => p.Preferencial))
                {
                    if (item.Ativo) { 
                        if (item.Preferencial)
                        {
                            pessoa.Posicao = item.Posicao + 1;
                        } else
                        {
                            item.Posicao = item.Posicao + 1;
                        }
                        await _pessoaRepository.Atualizar(item);
                    }
                }
                
            }
            else
            {
                pessoa.Posicao = pessoasdafila.Count + 1;
            }
            pessoa.FilaId = pessoaViewModel.filaId;
            //var fila = _mapper.Map<Fila>(newfila);
            //await _filaRepository.Adicionar(fila);
            //pessoa.FilaId = fila.Id;

            await _pessoaRepository.Adicionar(pessoa);


            if (!OperacaoValida()) return View(pessoaViewModel);

            return RedirectToAction("Details", new { id = pessoaViewModel.filaId });
        }

        // GET: FilaController/Edit/5
        public ActionResult Edit(int id)
        {
            ConfigEmpresa();
            return View();
        }

        // POST: FilaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            ConfigEmpresa();
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FilaController/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            ConfigEmpresa();
            var filatoopen = await _filaRepository.ObterPorId(id);

            filatoopen.Ativo = false;
            await _filaRepository.Atualizar(filatoopen);
            await _filaRepository.SaveChanges();
            return RedirectToAction(nameof(Index));
            //return View();
        }

        // POST: FilaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id, IFormCollection collection)
        {
            ConfigEmpresa();
            try
            {
                var filatoopen = await _filaRepository.ObterPorId(id);

                filatoopen.Ativo = false;
                await _filaRepository.Atualizar(filatoopen);
                await _filaRepository.SaveChanges();

                //await _filaRepository.Remover(id);
                //await _filaRepository.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
