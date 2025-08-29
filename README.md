# LibraryManagementNet9

Advanced Library Management System using **.NET 9**, **ASP.NET Core MVC**, **EF Core**, **Repository Pattern**, and **AJAX**.

## Features mapped to user stories

1. **Repository Pattern with EF Core**
   - `IGenericRepository<T>`, `GenericRepository<T>` with CRUD + includes + skip/take.
   - Specific repos: `BookRepository`, `AuthorRepository`, `GenreRepository`.

2. **CRUD with relationships**
   - Entities: `Book` (AuthorId, GenreId), `Author`, `Genre` with navigation properties.
   - Async methods everywhere.

3. **AJAX integration**
   - Modals for Create/Edit with partial forms, jQuery AJAX submit, JSON responses.
   - No full page reload for add/edit; delete confirms.

4. **Advanced queries + error handling**
   - Search, sort, pagination in `BookRepository.SearchAsync`.
   - Server error responses with meaningful messages; global Error view.

## Run (VS Code)

```bash
dotnet restore
dotnet build
dotnet run
```

Open `https://localhost:5001` (or the shown URL). The app creates `library.db` automatically and seeds sample data.
