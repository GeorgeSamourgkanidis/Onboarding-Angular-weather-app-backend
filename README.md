# Onboarding-Angular-weather-app-backend
This backend is required for: https://github.com/GeorgeSamourgkanidis/Onboarding-Angular-weather-app-frontend

Connected to my online postgresSql server

If the env is dev then I accept CORS to call the apis from my local frontend

created Models for FavoriteCities and Users

created one to many relationship users->cities

created migration using entityFramework using the commands "dotnet ef migrations add init" and "dotnet ef database update"

created seed data. The database can be seeded using the command "dotnet run seeddata"

created getFavoriteCities, save and unsave favorite cities apis.

Weather data are being fetched using an external Weather api and I returned only the needed data to the frontend.

Implement Jwt auth and get Username in any api called using httpContextAccessor. 

Now the favorite weather apis need JwtToken. Also the user table is changed and now the primary key is the Username column instead of Id. 

On google/facebook login I follow the same logic. I use their emails as Username and their id Bcrypted as a password.
