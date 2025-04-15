# Caching and Dynamic UI Application

## Project Overview
This application demonstrates caching strategies in ASP.NET Core using both Redis and in-memory caching mechanisms, combined with dynamic UI updates and optimized image loading. The application fetches data from an external API (JSONPlaceholder), caches it, and displays it using modern web techniques.

## Features
- **Multiple Caching Strategies:**
  - Redis distributed caching
  - Memory caching with IMemoryCache
- **Dynamic UI Updates:** JavaScript-based UI rendering without page reloads
- **Image Optimization:**
  - Lazy loading of images using IntersectionObserver
  - WebP format support with fallback for older browsers
  - Responsive image layouts
- **Responsive Design:** Bootstrap-based responsive layout

## Technical Architecture

### Backend
- **ASP.NET Core 8.0:** Modern web application framework
- **ICacheService:** Abstract interface for caching implementation
  - **RedisCacheService:** Redis implementation
  - **MemoryCacheService:** In-memory implementation
- **IApiService:** External API data retrieval
- **Controllers:**
  - **HomeController:** Renders views
  - **ApiDataController:** RESTful API endpoints for cached data

### Frontend
- **JavaScript:** Vanilla JS for dynamic updates and lazy loading
- **Bootstrap:** For responsive UI components
- **CSS Grid:** For the responsive image gallery

## Prerequisites
- .NET 8.0 SDK
- Redis server instance (local or cloud)
- Visual Studio 2022 or compatible IDE

## Getting Started

### Installation
1. Clone the repository
   ```
   git clone https://github.com/Raghad-Alahmadi/Caching-and-Dynamic-UI.git
   cd Caching-and-Dynamic-UI
   ```

2. Configure Redis connection string in appsettings.json
   ```json
   "ConnectionStrings": {
     "RedisConnection": "localhost:6379"
   }
   ```

3. Build the solution
   ```
   dotnet build
   ```

4. Run the application
   ```
   dotnet run
   ```

5. Open your browser and navigate to https://localhost:5001 or http://localhost:5000

### Configuration
The application can be configured through various settings in appsettings.json:
- `ConnectionStrings:RedisConnection`: Redis server connection details
- Cache timeout settings and other configurations

## Usage
The application provides several views:
- **Home:** Landing page with links to other pages
- **Users:** Displays user data with lazy-loaded images
- **Posts:** Displays post data from JSONPlaceholder

On the Users page:
- Click "Load from Redis" to retrieve user data from Redis cache
- Click "Load from Memory" to retrieve user data from memory cache
- Click "Clear Cache" to clear both caches

## API Endpoints
The application exposes several API endpoints:
- `GET /api/ApiData/redis/users`: Get users from Redis cache
- `GET /api/ApiData/memory/users`: Get users from memory cache
- `GET /api/ApiData/redis/posts`: Get posts from Redis cache
- `GET /api/ApiData/memory/posts`: Get posts from memory cache
- `DELETE /api/ApiData/redis/clear?key={key}`: Clear a specific key from Redis cache
- `DELETE /api/ApiData/memory/clear?key={key}`: Clear a specific key from memory cache

## Key Components

### Caching Implementations
- **RedisCacheService.cs:** Provides Redis caching implementation
- **MemoryCacheService.cs:** Provides in-memory caching implementation

### External API Service
- **ApiService.cs:** Fetches data from JSONPlaceholder API

### Controllers
- **HomeController.cs:** Handles page rendering
- **ApiDataController.cs:** Handles API requests with caching

### Views
- **Users.cshtml:** Displays user data with lazy-loaded images and WebP support
- **Posts.cshtml:** Displays post data

## Performance Optimizations
- **Lazy Loading:** Images load only when they enter the viewport
- **WebP Support:** Uses WebP format for supported browsers
- **Caching:** Reduces API calls and improves response times
- **Responsive Images:** Optimized for various device sizes

## Troubleshooting
- Ensure Redis server is running if using Redis caching
- Check browser console for JavaScript errors
- Verify API endpoints using a tool like Postman
- Check application logs for backend errors
  
## Acknowledgments
- JSONPlaceholder for providing the demo API
- StackExchange.Redis library
- Bootstrap for UI components
