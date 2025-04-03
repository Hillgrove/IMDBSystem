## IMDB System – 4. Semester Datamatiker  
**Valgfag: Advanced Databases**




Dette projekt er en del af den obligatoriske databaseopgave på 4. semester og simulerer et forsimplet IMDB-system, hvor brugeren kan søge i titler og personer, samt tilføje, opdatere og slette titler.

## 🧩 Funktionalitet

Brugerfladen er en konsolapplikation, hvor følgende funktioner er tilgængelige:

### 🎬 Titler
- Søg titler med wildcard og vis resultater alfabetisk (inkl. paginering)
- Se detaljeret information om en valgt titel
- Opdater titelens grunddata
- Slet titel
- Tilføj ny titel med:
  - Type (film, serie, osv.)
  - Titel (primær og original)
  - Voksenindhold
  - Udgivelsesår og evt. slutår
  - Spilletid
  - Genrer (op til 3)

### 👤 Personer
- Søg personer med wildcard og vis resultater alfabetisk (inkl. paginering)
- Tilføj ny person med:
  - Navn
  - Fødselsår
  - Dødsår (valgfrit)

## 🗃️ Teknologi

- **Sprog**: C# (.NET)
- **UI**: Konsol
- **Database**: SQL Server (via Stored Procedures)
- **Arkitektur**:
  - Repository pattern
  - Interfaces: `ITitleRepository`, `INameRepository`
  - Implementeringer: `TitleRepositoryList` (in-memory), `NameRepositorySql` (SQL)
- **Datahåndtering**:
  - Stored Procedures (e.g. `AddPerson`, `SearchPersonsSorted`)
  - Bruger har kun adgang til views/procedures – ingen direkte tabeladgang

## 🛡️ Sikkerhed

- Al databaseadgang sker gennem stored procedures
- Ingen dynamisk SQL – dermed sikret mod SQL Injection
- Brugeradgang er begrænset via rettigheder (ikke implementeret i kode, men beskrevet i dokumentation)

## 📁 Filstruktur (udvalg)

| Fil                      | Beskrivelse                                       |
|--------------------------|---------------------------------------------------|
| `Program.cs`             | Entry point med repo-initialisering               |
| `UI.cs`                  | Main menu og program flow                         |
| `TitleSearchMenu.cs`     | Søgning og CRUD af titler                         |
| `PersonSearchMenu.cs`    | Søgning efter personer                            |
| `AddTitleMenu.cs`        | Tilføj titler (med validering og genrevalg)       |
| `AddNameMenu.cs`         | Tilføj personer                                   |
| `NameRepositorySql.cs`   | SQL-adgang til personer                           |
| `TitleRepositoryList.cs` | Midlertidig in-memory repository for titler       |

## 🏗️ Videreudviklingsforslag

- Tilføjelse af cast/crew via `title.principals.tsv` (ekstra)
- Udvidet personvisning med involveringer (ekstra)
- Triggers og logning af dataændringer
- Brugerroller (read-only, editor, admin)
- Webbaseret UI med API-gateway
