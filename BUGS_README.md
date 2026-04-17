# Library System API - Bug Documentation

This is a buggy **Library Management System API** built with .NET and n-tier architecture. The system contains **20 intentional bugs** across different layers (Models, Repositories, Services, Controllers, and Configuration).

## Architecture

The application follows n-tier architecture:
- **Models Layer**: Data models (Book, Member, Transaction)
- **Repository Layer**: Data access logic
- **Service Layer**: Business logic
- **Controller Layer**: API endpoints
- **Program.cs**: Dependency injection configuration

---

## List of Bugs

### BUG #01: Missing Double-Quote

**Location:** `Repositories/MemberRepository.cs:23:94`<br>
**Type:** Syntax Error

```cs
new Member { Id = 3, Name = "Ken Martinez", Email = "jmartenezlopez@sjcoe.net, MembershipDate = DateTime.Now.AddMonths(-6) }
```

### BUG #02: Unnecessary Catch Clause

**Location:** `Controllers/BooksController.cs:69:13`<br>
**Type:** Logic Error

```cs
catch (ArgumentNullException ex)
{
    return BadRequest(ex.Message);
}
```

### BUG #03: Incorrect Assignment Operator '=' In Place Of Comparison Operator '=='

**Location:** `Repositories/BookRepositories.cs:37:52`<br>
**Type:** Syntax Error

```cs
return _books.FirstOrDefault(b => b.Id = id);
```

### BUG #04: Incorrect Assignment Operator '=' In Place Of Comparison Operator '=='

**Location:** `Repositories/MemberRepository.cs:40:57`<br>
**Type:** Syntax Error

```cs
return _members.FirstOrDefault(m => m.Email = email);
```

### BUG #05: Missing Services Registration For 'IMemberRepository', 'BookRepository'

**Location:** `Program.cs:9:1`<br>
**Type:** Configuration Error

```cs
builder.Services.AddSingleton<IMemberRepository, MemberRepository>();
builder.Services.AddSingleton<ITransactionRepository, TransactionRepository>();
```

### BUG #06: Services Registered CORS Policy 'AllowAll' Does Not Allow Any Origin

**Location:** `Program.cs:17:1`<br>
**Type:** Configuration Error

```cs
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyMethod()
               .AllowAnyHeader();
    });
});
```

### BUG #07: Missing Middleware Activation For CORS Policy 'AllowAll'

**Location:** `Program.cs:34:1`<br>
**Type:** Configuration Error

```cs
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
```

### BUG #08: Missing Validation For Field 'ISBN'

**Location:** `Services/BookService.cs:38:9`<br>
**Type:** Validation Error

```cs
public Book AddBook(Book book)
{
    if (book == null)
    {
        throw new ArgumentNullException(nameof(book));
    }

    if (string.IsNullOrWhiteSpace(book.Title))
    {
        throw new ArgumentException("Book title is required");
    }

    if (string.IsNullOrWhiteSpace(book.Author))
    {
        throw new ArgumentException("Book author is required");
    }

    return _bookRepository.Create(book);
}
```

### BUG #09: Missing Validation For Fields 'Title', 'Author', 'ISBN'

**Location:** `Services/BookService.cs:63:9`<br>
**Type:** Validation Error

```cs
public Book UpdateBook(int id, Book book)
{
    if (id <= 0)
    {
        throw new ArgumentException("Invalid book ID");
    }

    if (book == null)
    {
        throw new ArgumentNullException(nameof(book));
    }

    var existingBook = _bookRepository.GetById(id);
    if (existingBook == null)
    {
        throw new KeyNotFoundException($"Book with ID {id} not found");
    }

    return _bookRepository.Update(id, book);
}
```

### BUG #10: Partial Validation For 'memberId'

**Location:** `Services/LibraryService.cs:24:13`<br>
**Type:** Validation Error

```cs
var member = _memberRepository.GetById(memberId);
if (member == null)
{
    throw new KeyNotFoundException("Member not found");
}
```

### BUG #11: Partial Validation For 'bookId'

**Location:** `Services/LibraryService.cs:34:13`<br>
**Type:** Validation Error

```cs
var book = _bookRepository.GetById(bookId);
if (book == null)
{
    throw new KeyNotFoundException("Book not found");
}
```

### BUG #12: Partial Validation For 'transactionId'

**Location:** `Services/LibraryService.cs:68:13`<br>
**Type:** Validation Error

```cs
var transaction = _transactionRepository.GetById(transactionId);
if (transaction == null)
{
    throw new KeyNotFoundException("Transaction not found");
}
```

### BUG #13: Missing Validation For 'memberId'

**Location:** `Services/LibraryService.cs:92:9`<br>
**Type:** Validation Error

```cs
public List<Transaction> GetMemberTransactions(int memberId)
{
    return _transactionRepository.GetByMemberId(memberId);
}
```

### BUG #14: Missing Catch Clause For 'ArgumentException'

**Location:** `Controllers/LibraryController.cs:18:9`<br>
**Type:** Logic Error

```cs
[HttpPost("borrow")]
public ActionResult<Transaction> BorrowBook([FromBody] BorrowRequest request)
{
    try
    {
        var transaction = _libraryService.BorrowBook(request.MemberId, request.BookId);
        return Ok(transaction);
    }
    catch (KeyNotFoundException ex)
    {
        return NotFound(ex.Message);
    }
    catch (InvalidOperationException ex)
    {
        return BadRequest(ex.Message);
    }
}
```

### BUG #15: Missing Catch Clause For 'ArgumentException'

**Location:** `Controllers/LibraryController.cs:40:9`<br>
**Type:** Logic Error

```cs
[HttpPost("return/{transactionId}")]
public ActionResult<Transaction> ReturnBook(int transactionId)
{
    try
    {
        var transaction = _libraryService.ReturnBook(transactionId);
        return Ok(transaction);
    }
    catch (KeyNotFoundException ex)
    {
        return NotFound(ex.Message);
    }
    catch (InvalidOperationException ex)
    {
        return BadRequest(ex.Message);
    }
}
```

### BUG #16: Missing Exception Handling

**Location:** `Controllers/LibraryController.cs:62:9`<br>
**Type:** Logic Error

```cs
[HttpGet("transactions/member/{memberId}")]
public ActionResult<List<Transaction>> GetMemberTransactions(int memberId)
{
    var transactions = _libraryService.GetMemberTransactions(memberId);
    return Ok(transactions);
}
```

### BUG #17: Missing Validation For Fields 'Name', 'Email'

**Location:** `Services/MemberServices.cs:64:9`<br>
**Type:** Validation Error

```cs
public Member? UpdateMember(int id, Member member)
{
    if (id <= 0)
    {
        throw new ArgumentException("Invalid member ID");
    }

    if (member == null)
    {
        throw new ArgumentNullException(nameof(member));
    }

    var existingMember = _memberRepository.GetById(id);
    if (existingMember == null)
    {
        throw new KeyNotFoundException($"Member with ID {id} not found");
    }

    return _memberRepository.Update(id, member);
}
```

### BUG #18: Incorrect Less-Than Operator '<' In Place Of Less-Than-Or-Equal Operator '<='

**Location:** `Services/MemberServices.cs:97:20`<br>
**Type:** Logic Error

```cs
if (id < 0)
{
    throw new ArgumentException("Invalid member ID");
}
```

### BUG #19: Missing Handling For Null-Possible Function Return

**Location:** `Controllers/MembersController.cs:84:17`<br>
**Type:** Logic Error

```cs
var updatedMember = _memberService.UpdateMember(id, member);
return Ok(updatedMember);
```

### BUG #20: Missing Handling For Null-Possible Function Return

**Location:** `Controllers/BooksController.cs:76:17`<br>
**Type:** Logic Error

```cs
var updatedBook = _bookService.UpdateBook(id, book);
return Ok(updatedBook);
```

### BUG #21: Unmarked Null-Possible Function Signatures

**Location:** `Repositories/*` & `Services/*`<br>
**Type:** Lint Error

### BUG #22: Model Strings Not Marked As Required

**Location:** `Models/Book.cs` & `Models/Member.cs`<br>
**Type:** Lint Error
