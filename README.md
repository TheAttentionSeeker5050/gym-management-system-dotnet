# Gym Management System

Tags: In progress

## Database descriptions:

Welcome to our Gym Membership Management MVC Application, designed exclusively for gym employees. This intuitive console empowers staff to seamlessly add and manage gym members, oversee their memberships, and modify member accounts. Our application ensures efficient access to member information and billing details, streamlining the administrative aspects of gym management. It's the perfect tool for maintaining a high level of service while simplifying the complexities of gym membership administration.

---

## Requirements

### **Functional Requirements**

1. **Member Management**:
    - Add new gym members with personal details.
    - Edit existing member information.
    - Delete member accounts when necessary.
    - Search and view member profiles.
2. **Membership Management**:
    - Ability to assign different types of memberships (Regular, Premium, Platinum).
    - Track the start and end dates of memberships.
    - Update membership status (active, inactive).
3. **Billing Management**:
    - Generate and manage bills for members.
    - Support different billing cycles (monthly, yearly).
    - View payment history.
4. **User Authentication**:
    - Secure login for gym employees.
    - Role-based access control for different functionalities.
5. **Reporting**:
    - Generate reports on membership statistics.
    - Financial reporting for billing and payments.

### **Non-Functional Requirements**

1. **Usability**:
    - User-friendly interface for easy navigation.
    - Responsive design for compatibility with various devices.
2. **Performance**:
    - Fast response times for all user actions.
    - Efficient handling of large numbers of member records.
3. **Security**:
    - Secure handling of sensitive member data.
    - Compliance with data protection regulations.
    - Encrypted data transmission.
4. **Reliability**:
    - High availability of the system.
    - Data backup and recovery mechanisms.
5. **Scalability**:
    - Ability to handle an increasing number of users and data volume.
    - Easy to update and maintain.

---

## **Database Schemas**

### **GymMember Document**

Namespace: **`gym_management_system.Models`**

The **`GymMember`** document represents a gym member in the system. It includes personal details, membership information, biometric data, and billing details.

**Fields:**

- **`Id`** (string): Unique identifier for the member.
- **`UserName`** (string): Username of the member.
- **`Email`** (string): Email address of the member.
- **`FullName`** (string): Full name of the member.
- **`PhoneNumber`** (string): Contact phone number.
- **`Address`** (string): Residential address.
- **`DateOfBirth`** (DateTime): Date of birth, default to January 1, 2000.
- **`DateJoined`** (DateTime): Date the member joined the gym, default to the current date.
- **`Memberships`** (Membership[]): Array of Membership objects representing the member's subscriptions.
- **`BioMetrics`** (BioMetric[]): Array of BioMetric objects holding the member's biometric data.
- **`Bills`** (Bill[]): Array of Bill objects for financial transactions.

**Membership Types:**

- **`Regular`**: Access to gym facilities.
- **`Premium`**: Includes personalized meal plans, exercise routines, and quarterly medical checkups.
- **`Platinum`**: All Premium benefits plus bi-monthly medical checkups and weekly personal trainer sessions.

**Bill Types:**

- **`Monthly`**
- **`Yearly`**

### **Membership Subdocument**

Represents a membership subscription of a gym member.

**Fields:**

- **`Id`** (string): Unique identifier.
- **`MembershipType`** (string): Type of membership.
- **`MembershipStatusActive`** (bool): Indicates if the membership is active.
- **`MembershipStartDate`** (DateTime): Start date of the membership.
- **`MembershipEndDate`** (DateTime): End date of the membership.

### **BioMetric Subdocument**

Contains biometric data of a gym member.

**Fields:**

- **`Id`** (string): Unique identifier.
- **`Weight`** (float): Weight of the member.
- **`Height`** (float): Height of the member.
- **`BMI`** (float): Body Mass Index.
- **`BodyFat`** (float): Percentage of body fat.

### **Bill Subdocument**

Represents billing information for a gym member.

**Fields:**

- **`Id`** (string): Unique identifier.
- **`BillType`** (string): Type of bill (Monthly/Yearly).

### **User Document**

Namespace: **`gym_management_system.Models`**

The **`User`** document represents an employee user in the system, such as a gym manager or staff member.

**Fields:**

- **`Id`** (string): Unique identifier for the user.
- **`UserName`** (string): Username of the employee.
- **`UserPassword`** (string): Password for account access.
- **`Email`** (string): Email address of the employee.
- **`FullName`** (string): Full name of the employee.

## Site Map

### Welcome Screen

- **Description**: A welcoming landing page with a brief overview of the application.
- **Features**:
    - Login option for returning users.
    - Registration option for new users.
    - Demo sign-up for testing the application as a dummy user.

### User Authentication

- **Routes**:
    - **`/login`**: User login page.
    - **`/register`**: User registration page.

### Admin Dashboard (**`/admin`**)

- **Access**: Available upon successful user login.
- **Features**:
    - Header with options to manage gym employee account.
    - Search functionality to find registered members.
    - Main container displaying relevant page content.
- **Aside Section**:
    - "Add New Member" button: To register new gym members.
    - "Settings" button: Redirects to the settings page.

### Member Management

- **Base Route**: **`/admin/member`**
- **Subdirectories**:
    - **`/admin/member/{action}`**: Specific actions for member management, like biometrics and billing.
- **Search**: **`/admin/member?q`**: For searching current member accounts using a search bar.
- **List View**: **`/admin/member/list`**: Displays search results with optional query string parameters (e.g., **`q`**, filters, pagination).
    - Features pagination, adhering to the limit-offset pagination pattern.

### Settings Page

- **Route**: Included in **`/admin`** under the settings section.
- **Features**:
    - Divided into sections for specific settings.
    - Options include deleting gym members, changing member status, and modifying account data.
    - The user selects a section to view and edit.

### Account Management (**`/account`**)

- **Route**: **`/account/{action}`**
- **Features**:
    - Specific actions related to gym employee account management.

### Pagination and Filtering

- **Implementation**: Throughout the site, list views (such as member search, billing, biometrics measurements) feature pagination.
- **Pagination Style**: Default set by a fixed number, employing limit-offset pagination pattern for efficient data retrieval.

### General Navigation Flow

- Upon visiting the site, users are greeted with a welcome screen.
- Unauthenticated users have options to log in, register, or use a demo account.
- Authenticated users are directed to the admin dashboard.
- The admin dashboard serves as the central hub for all member and account management activities.
- Clear navigation through headers, aside sections, and structured routes ensures a user-friendly experience.