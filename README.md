# 供销e通 (YiTong Supply System)

A comprehensive multi-platform mobile and web application system for supply chain management.

## 📱 Project Overview

This project is a complete supply chain management solution consisting of Android mobile applications, a web-based management portal, and RESTful web services.

## 🏗️ Project Structure

```
dmy-yt-supply-system/
├── Android/                    # Main Android Application
│   ├── src/
│   │   └── com/damytech/
│   │       ├── YiTong/         # Main app activities
│   │       ├── CommService/    # Communication services
│   │       ├── HttpConn/       # HTTP client utilities
│   │       ├── STData/         # Data models
│   │       └── utils/          # Utility classes
│   ├── res/                    # Android resources
│   └── AndroidManifest.xml
│
├── YiTongWidget/               # Android Home Screen Widget
│   ├── src/
│   │   └── com/damytech/
│   │       └── yitongwidget/   # Widget components
│   └── res/
│
├── AppWidget/                  # Alternative Widget Implementation
│
├── Backend/                    # ASP.NET MVC Web Application
│   └── YiTongBackend/
│       ├── Controllers/        # MVC Controllers
│       │   ├── AccountController.cs
│       │   ├── AdvertController.cs
│       │   ├── BannerController.cs
│       │   ├── PostController.cs
│       │   ├── SettingController.cs
│       │   └── UploadController.cs
│       ├── Models/             # Data models
│       ├── Views/              # Razor views
│       └── Content/            # CSS, JS, images
│
├── WebService/                 # WCF REST API Service
│   └── YiTongWebService/
│       └── YiTongWebService/
│           ├── IService.cs     # Service contracts
│           ├── Service.svc.cs  # Service implementation
│           └── ServiceModel/   # Data models
│
├── Database/                   # Database files
│   └── YiTong_log.rar
│
└── Document/                   # Project documentation
    ├── Contract/               # Contract documents
    └── fromClient/             # Client-provided materials
```

## 🔧 Technology Stack

### Mobile (Android)
- **Language:** Java
- **Min SDK:** Android 2.2+ (implied from code)
- **Key Libraries:**
  - AsyncHttpClient - HTTP networking
  - SmartImageView - Image loading & caching
  - Baidu MobStat - Analytics

### Backend
- **Framework:** ASP.NET MVC 4
- **Language:** C#
- **Database:** SQLite (via LINQ to SQL)
- **UI Framework:** Bootstrap 3
- **Key Plugins:**
  - DataTables - Data tables
  - CKEditor - Rich text editor

### Web Service
- **Framework:** WCF (Windows Communication Foundation)
- **Protocol:** REST (JSON over HTTP)
- **Serialization:** Newtonsoft.Json

## 📡 API Endpoints

The WebService provides the following endpoints:

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/GetNewVersion?version={version}` | Check for app updates |
| GET | `/GetAdvertList` | Get advertisement list |
| GET | `/GetSiteAddr` | Get website address |
| GET | `/GetBannerList` | Get banner/carousel images |
| GET | `/GetSplashImgPath` | Get splash screen image |
| GET | `/GetPostList` | Get post/list content |

## 📱 Android Application Features

### Main Activities
- **WellComeActivity** - Splash/Welcome screen
- **MainActivity** - Main WebView-based interface
- **NotificationActivity** - Push notification handling

### Services
- **NewsService** - Background news/update service
- **BootReceiver** - Auto-start on device boot

### Key Capabilities
- WebView-based hybrid app
- Push notification support
- Auto-update functionality
- Image caching & loading
- Resolution adaptation

## 🌐 Web Backend Features

### Modules
- **Account Management** - User authentication & management
- **Advertisement Management** - Ad banner management
- **Banner Management** - Carousel/banner content
- **Post Management** - Content posting system
- **Settings** - System configuration
- **Upload** - File/media uploads

## 🚀 Getting Started

### Prerequisites
- Android Studio / Eclipse ADT for Android development
- Visual Studio 2012+ for .NET development
- IIS 7+ for web service deployment
- SQL Server or SQLite for database

### Building the Android App
1. Open `Android/` folder in Android Studio
2. Configure SDK paths
3. Build and deploy to device/emulator

### Setting up Backend
1. Open `Backend/YiTongBackend/YiTongBackend.sln` in Visual Studio
2. Configure database connection in `Web.config`
3. Build and publish to IIS

### Deploying WebService
1. Open `WebService/YiTongWebService/YiTongWebService.sln`
2. Configure database connection in `Web.config`
3. Publish to IIS

## 📄 License

This project contains proprietary software developed for 供销e通 (YiTong).

## 📝 Version History

- **v3.4** (September 2014) - Version from client delivery
- Development contract dated: September 12, 2014

