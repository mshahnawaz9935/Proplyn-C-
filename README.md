
This project has a C# file `Program.cs` which defines 6 methods to
load and process a couple of the [Houses of the Oireachtas Open Data APIs][1].

I have added the functionality for the user to input arguments through command line.

**Task 1:**

Added a method in project which accepts a url and returns back the response.

**Task 2:**

The old code used dynamic to process the key value pairs and was inefficient.
The best approach used to process Json data is to use pre-defined typed classes.

I created two classes named Legislation and Members that matches the JSON structure. 
Parsing generic JSON to a JSON.net JObject or generic dictionaries with FastJson is slower (~20%) than reading that data in to a defined class type. 
This is likely because a lot more meta data is tracked with the generic Json.NET’s JObject, JArray, JValue objects.

**Newtonsoft.Json.JsonConvert.DeserializeObject<List<MyType>(jsonData)** //faster with typed object
   
**Newtonsoft.Json.JsonConvert.DeserializeObject(jsonData)** //slower with generic JObject result

The old code for the filterSponsorByPID method had three loops for iterating over the data which could get very slow depending on the size of data.
So, after converting Json structure to classes it became very easy to use LINQ to query for the fullname or PID from a big tree. 
The new code uses LINQ and very much faster than the old code. I also used stopwatch to compare the time taken by the code block.
   
**Task 3 :**
   
 The unimplemnted method was implemented and it filters bills from the provided since and until date.
   
**Task 4:**
   
 Also I have added input arguments so that the user can use the command line to perform different operations like filtering and displaying bills.
   
  **Task 5:**
  Improved code readability and also added explanations and comments.

   The data loaded from the file was different than the data loaded from the api. So I amended the test cases to match my results. 
Apologies for sending the challenge late because I was very busy and caught up in many things.
