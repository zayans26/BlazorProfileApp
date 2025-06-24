# BTNX_Assignment


# BlazorProfileApp

A sample full-stack Blazor Server + Web API application for managing in-memory person profiles. This project was created as part of a technical interview assignment.

---

## 📌 Features

- 🧍 Add a new person profile
- ✏️ Update an existing profile by ID
- 📡 Data sent to and received from a Web API
- 🧠 In-memory data storage (no database)
- 🧩 Built with:
  - Blazor Server (.NET 7 or 8)
  - Minimal Web API
  - HTTP Client
  - Dependency Injection

---

## 🔧 Project Structure

```
BlazorProfileApp
│
├── BlazorProfileApp.UI # Blazor Server frontend (UI)
│ └── Pages
│ ├── ProfileForm.razor # Page to add/update profile
│ └── Index.razor # Home page
│
├── BlazorProfileApp.API # ASP.NET Core Minimal API backend
│ └── Models
│ └── PersonProfile.cs
│
└── BlazorProfileApp.sln # Solution file
```

---

## 🚀 Getting Started

### Prerequisites

- [.NET 7 SDK or later](https://dotnet.microsoft.com/download)
- Visual Studio 2022+ or VS Code
- (Optional) Postman for testing the API

---

### 🛠️ Run Instructions

1. **Clone the repo**

```bash
git clone https://github.com/zayans26/BlazorProfileApp.git
cd BlazorProfileApp
```

2. **Run the API**

```bash
cd BlazorProfileApp.API
dotnet run --launch-profile https
```

3. **Run the UI**

In a new terminal:

```bash
cd BlazorProfileApp.UI
dotnet run
```

4. Open the Blazor app in your browser (usually https://localhost:7111)

---

## 🧪 API Endpoints

| Method | Endpoint              | Description            |
|--------|-----------------------|------------------------|
| POST   | `/api/profile`        | Add a new profile      |
| PUT    | `/api/profile/{id}`   | Update a profile by ID |
| GET    | `/api/profile/{id}`   | Get a profile by ID    |
| GET    | `/api/profile`        | Get All Profiles       |

---

## ✅ Sample JSON Payload

```json
{
  "id": 1,
  "firstName": "John",
  "lastName": "Doe",
  "email": "john@example.com",
  "phoneNumber": "1234567890"
}
```

---

## 📌 Notes

- Data is stored **in memory only**. It resets every time the API restarts.
---



