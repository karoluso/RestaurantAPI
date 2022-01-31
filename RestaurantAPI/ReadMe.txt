1. How to add dishes while creating a restaurant.

2. How to catch and log all bad requests etc.


3. swagger dodanie w startup poprzez app, fluent validation dodtanie w startup inaczej, serwisy jeszcze inaczej -0 jak to jest z tym dodawaniemk?
   Nlog jeszcze inaczej jest dodany, FluentValidation jescze jest dodane do AddControlers

4.dalczego zamiszczamy wlasciwosci authentykacji w appsettings a nie w klasie.

   =========================
   > public IConfiguration Configuration { get; } it's in a  Startup and using it we can relate to 
     the appsettings.json;
     Configuration.GetSection("Authentication").Bind(authenticationSettings)  we bind created class  
     the properties that corresponds to those written in appsettings.json. GetSection - we need to give a name 
     of the section from the appsettings.json;



     Add validation fro LoginUserDto

     Doczytac pytanie Co dalej !! z pytan kursu.