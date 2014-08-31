//SCHEDULE-TIME-SPAN: 0 5 0
var fileName = @"C:\app\app.csx-"+System.DateTime.Now.ToString().Replace('/','-').Replace(':','-');
Console.WriteLine("Creating file " + fileName);
System.IO.File.CreateText(fileName);
Console.WriteLine("Hello ISaac!")