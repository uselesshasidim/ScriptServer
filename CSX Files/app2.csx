var fileName = @"C:\app\app2.csx-"+System.DateTime.Now.ToString().Replace('/','-').Replace(':','-');
Console.WriteLine("Creating file " + fileName);
System.IO.File.CreateText(fileName);