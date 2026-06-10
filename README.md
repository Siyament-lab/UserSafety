# UserSafety - Register & Login API

Ett REST API med Razor Pages UI för användarregistrering och inloggning med JWT-autentisering.

## Teknikstack
- ASP.NET Core 9 Web API
- Razor Pages (frontend)
- Entity Framework Core
- SQL Server LocalDB
- BCrypt.Net
- JWT Bearer Authentication

## Krav
- Visual Studio 2022
- .NET 9 SDK
- SQL Server LocalDB (ingår med Visual Studio)

## Kom igång

1. Klona repot
2. Öppna `UserSafety.sln` i Visual Studio
3. Högerklicka på `UserSafetyAPI` → **Manage User Secrets** och lägg till:
```json
{
  "Jwt": {
    "Key": "Din-hemliga-nyckel-minst-32-tecken-lång!"
  }
}
```
4. Notera vilken port **UserSafetyAPI** startar på och uppdatera `BaseAddress` i
   `UserSafetyWeb/Program.cs` om den skiljer sig från `https://localhost:7252`
5. Sätt upp Multiple Startup Projects:
   - Högerklicka på **Solution** → **Configure Startup Projects**
   - Välj **Multiple startup projects**
   - Sätt både `UserSafetyAPI` och `UserSafetyWeb` till **Start**
6. Tryck F5 för att starta båda projekten
7. Databasen skapas automatiskt vid uppstart
8. Navigera till webbappens port i webbläsaren (visas i terminalen vid uppstart)

## Användargränssnitt
Registrera och logga in via webbläsaren med Register och Login i navigationsmenyn.