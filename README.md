# 📱 Pokemon - Mobile Pokedex App

**Cross-Platform Mobile Application for Pokemon Exploration**

A modern mobile application showcasing real-time API integration, responsive UI design, and cross-platform development using C#. Explore Pokemon data with detailed statistics, images, and abilities.

## ✨ Features

✅ **Pokemon Database** - Browse complete Pokedex with 900+ Pokemon  
✅ **Search & Filter** - Find Pokemon by name, type, and abilities  
✅ **Detailed Stats** - View HP, Attack, Defense, Speed, and special stats  
✅ **Type System** - Filter by Pokemon types (Fire, Water, Electric, etc)  
✅ **Images & Artwork** - High-quality Pokemon sprites and artwork  
✅ **Responsive Design** - Works seamlessly on phones and tablets  
✅ **Offline Support** - Cache data for offline browsing  
✅ **Cross-Platform** - Android and iOS with single codebase  

## 🛠️ Tech Stack

- **Framework:** C# .NET, .NET MAUI (or React Native)
- - **API:** RESTful API integration (PokeAPI)
  - - **Frontend:** XAML, CSS styling, responsive layouts
    - - **Architecture:** MVVM pattern with ViewModel binding
      - - **Database:** Local SQLite for caching
        - - **Development:** Visual Studio, Android Studio emulator
         
          - ## 📋 Project Structure
         
          - ```
            Pokemon/
            ├── Models/              # Pokemon data models
            │   ├── Pokemon.cs
            │   ├── PokemonType.cs
            │   └── PokemonStats.cs
            ├── ViewModels/          # MVVM ViewModels
            │   ├── PokemonListViewModel.cs
            │   └── PokemonDetailViewModel.cs
            ├── Views/               # UI screens
            │   ├── PokemonListPage.xaml
            │   ├── PokemonDetailPage.xaml
            │   └── SearchPage.xaml
            ├── Services/            # API & Data services
            │   ├── PokemonApiService.cs
            │   ├── CacheService.cs
            │   └── SearchService.cs
            ├── Resources/           # Images, styles
            └── App.xaml            # App configuration
            ```

            ## 🚀 Quick Start

            ### Prerequisites
            - .NET 8 SDK
            - - Visual Studio 2022 or JetBrains Rider
              - - Android SDK (for Android development)
                - - Xcode (for iOS development - macOS only)
                 
                  - ### Setup
                 
                  - ```bash
                    # Clone repository
                    git clone https://github.com/robyxo/Pokemon.git
                    cd Pokemon

                    # Restore NuGet packages
                    dotnet restore

                    # Build for Android
                    dotnet build -f net8.0-android

                    # Build for iOS (macOS only)
                    dotnet build -f net8.0-ios

                    # Run on Android emulator
                    dotnet build -f net8.0-android -t run
                    ```

                    ### Using the App

                    1. **Launch** → Open the app on your device
                    2. 2. **Browse** → Swipe through Pokemon list
                       3. 3. **Search** → Use search bar to find specific Pokemon
                          4. 4. **Details** → Tap Pokemon to view full stats
                             5. 5. **Filter** → Choose Pokemon type to narrow results
                               
                                6. ## 📊 API Integration
                               
                                7. **PokeAPI (pokeapi.co)**
                                8. ```csharp
                                   var pokemonService = new PokemonApiService();
                                   var pokemon = await pokemonService.GetPokemonAsync(1); // Bulbasaur
                                   var allPokemon = await pokemonService.GetAllPokemonAsync();
                                   ```

                                   **Response Structure:**
                                   ```json
                                   {
                                     "id": 1,
                                     "name": "bulbasaur",
                                     "type": ["grass", "poison"],
                                     "stats": {
                                       "hp": 45,
                                       "attack": 49,
                                       "defense": 49,
                                       "speed": 45
                                     },
                                     "image_url": "https://..."
                                   }
                                   ```

                                   ## 🎨 UI Highlights

                                   - **List View** - Infinite scroll with Pokemon thumbnails
                                   - - **Detail View** - Full Pokemon information with stats charts
                                     - - **Search Bar** - Real-time search with debouncing
                                       - - **Type Tags** - Color-coded type badges
                                         - - **Loading States** - Skeleton loaders for smooth UX
                                          
                                           - ## 💡 Performance Optimization
                                          
                                           - - API response caching (24-hour expiry)
                                             - - Image lazy loading
                                               - - Virtual scrolling for large lists
                                                 - - Async/await for non-blocking UI
                                                   - - SQLite local database for offline access
                                                    
                                                     - ## 🔄 MVVM Architecture
                                                    
                                                     - ```csharp
                                                       // ViewModel example
                                                       public class PokemonListViewModel : INotifyPropertyChanged
                                                       {
                                                           private readonly IPokemonApiService _apiService;

                                                           public ObservableCollection<Pokemon> Pokemons { get; }

                                                           public async Task SearchPokemonAsync(string query)
                                                           {
                                                               var results = await _apiService.SearchAsync(query);
                                                               // Update UI
                                                           }
                                                       }
                                                       ```

                                                       ## 🧪 Features in Development

                                                       - [ ] Favorite Pokemon bookmarking
                                                       - [ ] - [ ] Comparison tool (compare 2 Pokemon)
                                                       - [ ] - [ ] Evolution chain display
                                                       - [ ] - [ ] Move database integration
                                                       - [ ] - [ ] Battle simulator (basic)
                                                       - [ ] - [ ] User ratings & reviews
                                                      
                                                       - [ ] ## 📸 Screenshots
                                                      
                                                       - [ ] *Screenshots coming soon*
                                                      
                                                       - [ ] ## 🚢 Deployment
                                                      
                                                       - [ ] **Android:**
                                                       - [ ] ```bash
                                                       - [ ] dotnet publish -f net8.0-android -c Release
                                                       - [ ] # APK generated at bin/Release/net8.0-android/publish/
                                                       - [ ] ```
                                                      
                                                       - [ ] **iOS:**
                                                       - [ ] ```bash
                                                       - [ ] dotnet publish -f net8.0-ios -c Release
                                                       - [ ] # IPA generated for App Store submission
                                                       - [ ] ```
                                                      
                                                       - [ ] ## 📞 Learning Outcomes
                                                      
                                                       - [ ] This project demonstrates:
                                                       - [ ] - ✅ Cross-platform mobile development
                                                       - [ ] - ✅ REST API consumption
                                                       - [ ] - ✅ MVVM architectural pattern
                                                       - [ ] - ✅ Data binding and observables
                                                       - [ ] - ✅ Local caching strategies
                                                       - [ ] - ✅ Responsive UI design
                                                       - [ ] - ✅ Async/await programming
                                                      
                                                       - [ ] ## 🤝 Contributing
                                                      
                                                       - [ ] Learning project - feel free to fork and extend!
                                                      
                                                       - [ ] ## 📝 License
                                                      
                                                       - [ ] MIT License - Open for educational use
                                                      
                                                       - [ ] ---
                                                      
                                                       - [ ] **Built as a learning project to master cross-platform mobile development**
                                                      
                                                       - [ ] *Last Updated: September 2026*
