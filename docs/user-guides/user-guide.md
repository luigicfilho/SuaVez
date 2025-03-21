# User Guide for .NET C# Application

This guide provides step-by-step instructions on how to use the .NET C# application, both as a customer joining a queue and as an administrator managing the system.

## Table of Contents

* [Joining a Queue (Customers)](#joining-a-queue-customers)
* [Tracking Your Position (Customers)](#tracking-your-position-customers)
* [Customizing the UI (Admins)](#customizing-the-ui-admins)
* [Setting Up Priority Rules (Admins)](#setting-up-priority-rules-admins)

## Joining a Queue (Customers)

1.  **Open the Application:** Open your web browser or the application's mobile app and navigate to the main page.

2.  **Select a Queue:** You'll see a list of available queues. Select the queue you want to join.

    ![Queue Selection Screenshot](placeholder_queue_selection.png)
    *Example: If you need customer support, select the "Customer Support" queue.*

3.  **Enter Required Information:** You may be asked to enter some information, such as your name, contact details, or reason for joining the queue.

    ![Information Input Screenshot](placeholder_information_input.png)
    *Example: Enter your name and a brief description of your issue.*

4.  **Join the Queue:** Click the "Join Queue" or "Submit" button.

5.  **Receive Your Queue Number:** You'll receive a queue number or ticket. This number will be used to track your position in the queue.

    ![Queue Number Display Screenshot](placeholder_queue_number.png)

## Tracking Your Position (Customers)

1.  **View Queue Status:** The application will display the current queue status, including your position and estimated wait time.

    ![Queue Status Screenshot](placeholder_queue_status.png)
    *Example: You are number 5 in the queue. Estimated wait time: 15 minutes.*

2.  **Real-Time Updates:** The application will provide real-time updates on your position. You may also receive notifications when your turn is approaching.

3.  **Digital Displays:** In physical locations, digital displays will show the current queue status and call out queue numbers.

    ![Digital Display Screenshot](placeholder_digital_display.png)

## Customizing the UI (Admins)

1.  **Access Admin Panel:** Log in as an administrator and navigate to the admin panel or settings section.

2.  **Go to Appearance/Theme Settings:** Find the "Appearance," "Theme," or "Customization" section.

3.  **Choose a Theme:** Select a predefined theme or create a custom theme.

    ![Theme Selection Screenshot](placeholder_theme_selection.png)

4.  **Customize Colors and Fonts:** Adjust the colors, fonts, and layout elements to match your brand or preferences.

    ![Color and Font Customization Screenshot](placeholder_color_font.png)

5.  **Upload Logo:** Upload your company or organization logo.

    ![Logo Upload Screenshot](placeholder_logo_upload.png)

6.  **Configure Display Options:** Customize the information displayed on queue display screens, such as queue names, estimated wait times, and advertisements.

7.  **Save Changes:** Click the "Save" or "Apply" button to save your changes.

## Setting Up Priority Rules (Admins)

1.  **Access Queue Management:** Log in as an administrator and navigate to the "Queue Management" or "Queues" section.

2.  **Select a Queue:** Choose the queue for which you want to set up priority rules.

3.  **Go to Priority Settings:** Find the "Priority Rules" or "Priority Levels" settings.

4.  **Create Priority Levels:** Define priority levels, such as "High," "Medium," and "Low."

    ![Priority Levels Screenshot](placeholder_priority_levels.png)

5.  **Set Priority Rules:** Configure rules to assign priority levels based on specific criteria, such as customer type, service type, or time of day.

    ![Priority Rules Configuration Screenshot](placeholder_priority_rules.png)
    *Example: Customers with VIP status receive high priority.*
    *Example: Urgent issues receive high priority.*

6.  **Save Changes:** Click the "Save" or "Apply" button to save your priority rules.

**Diagram Example (Queue Flow):**

```mermaid
graph TD
    A[Customer Joins Queue] --> B{Priority Rules?};
    B -- Yes --> C[Assign Priority];
    B -- No --> D[Normal Queue Position];
    C --> D;
    D --> E[Wait in Queue];
    E --> F[Service Agent Available];
    F --> G[Serve Customer];
