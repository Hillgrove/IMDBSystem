# IMDB System

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)
![C#](https://img.shields.io/badge/C%23-13.0-239120?logo=csharp)
![SQL Server](https://img.shields.io/badge/SQL%20Server-2022-CC2927?logo=microsoftsqlserver)
![Azure Key Vault](https://img.shields.io/badge/Azure%20Key%20Vault-0078D4?logo=microsoftazure)

Et konsol-baseret C#-system der simulerer et forsimplet IMDB-system. Brugere kan søge i film og serier samt tilhørende personer, og udføre fuld CRUD på titler og tilføje nye personer. Al databaseadgang sker udelukkende via stored procedures mod en SQL Server-database.

Projektet er udarbejdet som obligatorisk opgave på 4. semester, valgfaget Advanced Databases, forår 2025.

---

## Indholdsfortegnelse

- [Funktioner](#funktioner)
- [Teknologier](#teknologier)
- [Kom i gang](#kom-i-gang)
  - [Forudsætninger](#forudsætninger)
  - [Installation](#installation)
  - [Databaseopsætning](#databaseopsætning)
  - [Forbindelsesstreng](#forbindelsesstreng)
- [Projektstruktur](#projektstruktur)

---

## Funktioner

### Titler
- Søg titler med wildcard og filtrer på titeltype
- Bladr i resultater via paginering, sorteret alfabetisk
- Se detaljerede oplysninger om en enkelt titel
- Opdater en titels grunddata (type, navn, årstal, spilletid, genrer)
- Slet en titel fra databasen
- Tilføj ny titel med type, primær- og originaltitel, voksenindhold, udgivelses- og slutår, spilletid samt op til tre genrer

### Personer
- Søg personer med wildcard og bladr i resultater via paginering
- Tilføj ny person med navn, fødselsår og valgfrit dødsår

### Sikkerhed
- Al databaseadgang sker gennem stored procedures — ingen direkte tabeladgang
- Ingen dynamisk SQL, hvilket eliminerer risikoen for SQL Injection
- Forbindelsesstreng hentes fra Azure Key Vault via `DefaultAzureCredential`, eller kan sættes manuelt til lokal udvikling

---

## Teknologier

| Komponent           | Teknologi / Pakke                        | Version  |
|---------------------|------------------------------------------|----------|
| Sprog               | C#                                       | 13.0     |
| Platform            | .NET                                     | 8.0      |
| Database            | Microsoft SQL Server                     | 2022     |
| SQL-klient          | Microsoft.Data.SqlClient                 | 6.0.1    |
| Hemmeligheder       | Azure Key Vault + Azure.Identity         | 4.7 / 1.13 |
| Brugerflade         | Konsolapplikation                        | —        |

---

## Kom i gang

### Forudsætninger

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Microsoft SQL Server (lokalt eller i skyen)
- Adgang til en Azure Key Vault **eller** mulighed for at sætte forbindelsesstrengen manuelt (se nedenfor)

### Installation

```bash
git clone https://github.com/Hillgrove/IMDBSystem.git
cd IMDBSystem
dotnet restore
```

### Databaseopsætning

Databasen skal indeholde de stored procedures og views, som systemet benytter. De centrale procedurer er:

| Procedure / View         | Formål                                      |
|--------------------------|---------------------------------------------|
| `dbo.SearchTitlesSorted` | Søg og paginer titler                       |
| `dbo.AddTitle`           | Tilføj ny titel                             |
| `dbo.UpdateTitle`        | Opdater eksisterende titel                  |
| `dbo.DeleteTitle`        | Slet titel                                  |
| `dbo.SearchPersonsSorted`| Søg og paginer personer                     |
| `dbo.AddPerson`          | Tilføj ny person                            |
| `dbo.AllTypes`           | View over gyldige titeltyper                |
| `dbo.AllGenres`          | View over gyldige genrer                    |

### Forbindelsesstreng

Systemet understøtter to måder at angive forbindelsesstrengen på:

**Mulighed 1 — Azure Key Vault (standard)**

Sørg for at din bruger har adgang til Key Vault og er logget ind via Azure CLI (`az login`). Vault-URI og hemmelighedens navn er konfigureret i `Program.cs`.

**Mulighed 2 — Manuel konfiguration (til lokal udvikling)**

Åbn [ConsoleUI/Program.cs](ConsoleUI/Program.cs) og udfyld de tre variabler øverst i filen:

```csharp
string serverName = "din-server";
string userId = "dit-brugernavn";
string password = "din-adgangskode";
```

Når alle tre er udfyldt, anvendes den manuelle forbindelsesstreng frem for Key Vault.

Kør derefter projektet:

```bash
dotnet run --project ConsoleUI
```

---

## Projektstruktur

```
IMDBSystem/
├── IMDBSystem.sln
├── ConsoleUI/                        # Præsentationslag (konsolapplikation)
│   ├── Program.cs                    # Entry point — initialiserer repositories og starter UI
│   ├── UI.cs                         # Hovedmenu og overordnet programflow
│   ├── TitleSearchMenu.cs            # Søgning, visning og CRUD af titler
│   ├── NameSearchMenu.cs             # Søgning og visning af personer
│   ├── AddTitleMenu.cs               # Guidet oprettelse af ny titel
│   ├── AddNameMenu.cs                # Guidet oprettelse af ny person
│   └── Helpers/
│       └── ConsoleFormatter.cs       # Formatering og layout af konsol-output
└── Data/                             # Datalag
    ├── ITitleRepository.cs           # Interface for titeloperationer
    ├── INameRepository.cs            # Interface for personoperationer
    ├── TitleRepositorySql.cs         # SQL Server-implementering af ITitleRepository
    ├── TitleRepositoryList.cs        # In-memory-implementering (til test)
    ├── NameRepositorySql.cs          # SQL Server-implementering af INameRepository
    ├── NameRepositoryList.cs         # In-memory-implementering (til test)
    └── Models/
        ├── Title.cs                  # Datamodel for en titel
        ├── Name.cs                   # Datamodel for en person
        ├── Genre.cs                  # Datamodel for en genre
        └── IMDBType.cs               # Datamodel for en titeltype
```
