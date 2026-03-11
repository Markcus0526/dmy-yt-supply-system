# YiTong Supply System

A comprehensive supply chain management system with Android mobile applications, web backend admin panel, and REST API services.

## 📋 Project Overview

**YiTong** is a B2B/B2C supply chain management platform that provides:
- Android mobile applications for supply management
- Web-based admin dashboard for content management
- REST API services for mobile-backend communication

## 🏗️ Project Structure

```
dmy-yt-supply-system/
├── Android/                    # Main Android Application
│   ├── src/com/damytech/       # Source code
│   │   ├── YiTong/             # Main app activities
│   │   ├── CommService/        # Communication services
│   │   ├── HttpConn/           # HTTP client (AsyncHttpClient)
│   │   ├── STData/             # Data models
│   │   └── utils/              # Utility classes
│   ├── res/                    # Android resources
│   └── AndroidManifest.xml     # App manifest
│
├── AppWidget/                  # Android App Widget
│   └── src/com/damytech/appwidget/
│
├── YiTongWidget/               # YiTong Widget Component
│   └── src/com/damytech/yitongwidget/
│
├── Backend/YiTongBackend/      # ASP.NET MVC Web Backend
│   ├── Controllers/            # MVC Controllers
│   │   ├── AccountController.cs
│   │   ├── AdvertController.cs
│   │   ├── BannerController.cs
│   │   ├── PostController.cs
│   │   └── SettingController.cs
│   ├── Models/                 # Data models & LINQ
│   ├── Views/                  # Razor views
│   └── Content/                # CSS, JS, images
│
├── WebService/YiTongWebService/  # WCF REST API Service
│   └── YiTongWebService/
│       ├── IService.cs         # Service interface
│       ├── Service.svc.cs      # Service implementation
│       └── ServiceModel/       # Service data models
│
├── Database/                   # Database files
│   └── YiTong_log.rar          # SQLite database backup
│
└── Document/                   # Project documentation
    ├── Contract/               # Contract documents
    └── fromClient/             # Client-provided materials
```

## 🔌 API Endpoints

The WebService provides the following REST API endpoints (JSON):

| Endpoint | Description |
|----------|-------------|
| `GetNewVersion` | Check for app updates |
| `GetAdvertList` | Get advertisement list |
| `GetSiteAddr` | Get site addresses |
| `GetBannerList` | Get banner images |
| `GetSplashImgPath` | Get splash screen image path |
| `GetPostList` | Get posts/articles list |

### Example API Request
```
GET /Service.svc/GetAdvertList
```

## 💻 Technology Stack

### Backend
- **Framework**: ASP.NET MVC 4, WCF (Windows Communication Foundation)
- **Language**: C# (.NET Framework)
- **ORM**: LINQ to SQL
- **Database**: SQLite

### Web Service
- **Framework**: WCF REST API
- **Format**: JSON (using Newtonsoft.Json)
- **Authentication**: Custom token-based

### Mobile Applications
- **Platform**: Android (Java)
- **HTTP Client**: AsyncHttpClient
- **Image Loading**: Custom SmartImageView implementation

### Frontend (Admin Panel)
- **Framework**: Bootstrap 3
- **JavaScript**: jQuery, DataTables
- **CSS**: Metronic Admin Template

## 🚀 Getting Started

### Prerequisites

- Windows Server / IIS for Web Backend
- .NET Framework 4.5+
- Visual Studio 2012+ (for backend development)
- Android Studio / Eclipse (for Android development)
- SQLite database

### Building the Backend

1. Open `Backend/YiTongBackend/YiTongBackend.sln` in Visual Studio
2. Restore NuGet packages
3. Configure connection strings in `Web.config`
4. Build and deploy to IIS

### Building the Web Service

1. Open `WebService/YiTongWebService/YiTongWebService.sln`
2. Configure database connection in `Web.config`
3. Build and deploy to IIS

### Building Android Apps

1. Import Android/ or YiTongWidget/ project to Android Studio
2. Update `GlobalData.java` with your server URLs
3. Build and install on Android device/emulator

## 📱 Android Modules

### Main App (Android/)
- `MainActivity.java` - Main application entry
- `WellComeActivity.java` - Splash screen
- `NewsService.java` - Background news service
- `NotificationActivity.java` - Push notification handling

### App Widget (AppWidget/)
- `WidgetProvider.java` - Home screen widget
- `WidgetIntentReceiver.java` - Widget broadcast receiver

### YiTong Widget (YiTongWidget/)
- Similar to AppWidget with additional features

## 🔧 Configuration

### Backend Configuration
Edit `ConnStrings.config` in both Backend and WebService projects:

```xml
<connectionStrings>
    <add name="YiTongDB" connectionString="Data Source=...;Version=3;" />
</connectionStrings>
```

### Android Configuration
Update `GlobalData.java`:

```java
public class GlobalData {
    public static String DOMAIN = "http://your-server.com";
    public static String BASE_URL = DOMAIN + "/Service.svc/";
}
```

## 📄 License

This project contains proprietary code. All rights reserved.

## 📅 Project History

- **2014-07-24**: Initial contract documents
- **2014-09-01**: Android v3.4 release
- **2014-09-12**: Phase 2 development contract

## 👥 Project Components

| Component | Description |
|-----------|-------------|
| Main App | Supply chain management mobile app |
| App Widget | Home screen widget for quick access |
| YiTong Widget | Enhanced widget functionality |
| Admin Panel | Web-based content management |
| REST API | Mobile-backend communication |

