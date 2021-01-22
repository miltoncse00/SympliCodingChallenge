# SympliCodingChallenge

I have solved the coding challenge and added solution for extension 1 and extension 2.

I have used .NET core framework. I have added following features here

- Swagger for Testing and UI.
- Middleware for Exception Handling and Logging
- Caching using .NET core MemoryCache.
- NSubstitute for Mocking (similar to Moq)
- Strategy Patterns to implement Searching logic.

Swagger page [https://localhost:44308/swagger](https://localhost:44308/swagger) provided here request and response.

**POST**  **&quot;https://localhost:44308/api/Search**

Schema of the request is

**{**

**&quot;keyword&quot;:**  **&quot;e settlement&quot;**** ,**

**&quot;site&quot;:**  **&quot;sympli.com&quot;**** ,**

**&quot;searchEngineType&quot;:**  **&quot;Google&quot;**

**}**

And Response

**{**

"positions"* : "2,3",

**&quot;searchEngineType&quot;**** : "Google"

**}**
Rendered
Coding Challenge
I have solved the coding challenge and added solution for extension 1 and extension 2.

I have used .NET core framework. I have added following features here

Swagger for Testing and UI.
Middleware for Exception Handling and Logging
Caching using .NET core MemoryCache.
NSubstitute for Mocking (similar to Moq)
Strategy Patterns to implement Searching logic.
Swagger page https://localhost:44308/swagger provided here request and response.

POST "https://localhost:44308/api/Search

Schema of the request is

{

"keyword": "e settlement" ,

"site": "sympli.com" ,

"searchEngineType": "Google"

}

And Response

{

"positions" : "2,3",

"searchEngineType"** : "Google"

}


