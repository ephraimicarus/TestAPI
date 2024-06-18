## Laundry management application
- #### A work in progress, I use this repo as a blueprint for a real-life production application which I am developing
- The actual production code is optimised and implemented with the mainstream best practices (SOLID, DRY, Repository pattern), where applicable
- Also, the production API and database are hosted on Azure, with all bindings kept "secret", thanks to the Azure platform

### What the user wanted
- Keep track of laundry merchandise (bed-sheets, pillow cases, etc.)
- Know which customer has which merchandise at any given moment. The laundry shop rents out merchandise, with a 7-day free period upon contract beginning
- Notify User of any violated contracts (free rent period violations)

### Technical features
- User can execute CRUD operations for Customers and Items
- Business logic automated through API calls (MAUI application, ASP .NET core API)
- Notifications implemented interally (no server overhead for push notifications)  
