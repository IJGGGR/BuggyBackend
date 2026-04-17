# Library System API - Bug Documentation

This is a buggy Library Management System API built with .NET and n-tier architecture. The system contains **20 intentional bugs** across different layers (Models, Repositories, Services, Controllers, and Configuration).

## Architecture

The application follows n-tier architecture:
- **Models Layer**: Data models (Book, Member, Transaction)
- **Repository Layer**: Data access logic
- **Service Layer**: Business logic
- **Controller Layer**: API endpoints
- **Program.cs**: Dependency injection configuration

---

## Please List all of the bugs you find & where you found it


### EXMPLE **Bug #1: Wrong HTTP Request method in MemberController**
**Location**: `Controllers/MemberController.cs` - Line 38  
**Type**: API Design Error  

**Bug**:
```csharp
    [HttpDelete("{id}")]
    public ActionResult<Member> Update(int id, [FromBody] Member member)
```

