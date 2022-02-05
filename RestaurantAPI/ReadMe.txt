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

     > app.UseRouting();
      app.UseAuthorization(); ( must be between userouting and useendpoints);
      app.UseEndpoints(endpoints =>
      
      >controller  inherits from ControlerBase class that contains numerous properties and methods , among them there is a
        claim principal so we have the access to the caims from the controller class.

     Add validation fro LoginUserDto

     date of birth  policy - check how to avoid null excep if no dob is goven while registering user

     Add separate log file for logging info only 





     Doczytac pytanie Co dalej !! z pytan kursu.
      
     Jak opanuję to wszystko na  dobrym poziomie, to mogę myśleć już o szukaniu jakiejś pracy? Czy to jeszcze za mało? Pytam, bo z jednej strony widzę, że z tą wiedzą można już coś fajnego zrobić, ale z drugiej strony mam 19 lat dopiero i nie chcę mi się wierzyć, że już bym mógł coś tam działać...
