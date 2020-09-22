Solution for the third project:
I created an asp .net web application (.net framework) with the web API template. The solution includes controller called weather. The weather controller consumes two services. 
•	 GET api/Weather: 
This service will return all stations with related details.
•	GET api/Weather/94672 (id)
This service will return specific details of a station(will work with the passed id)

Structure:
The main responsibility for retrieving data is for a class called Webservice.cs. 
This class consumes a generic method called requestURL to call the URL and retrieve data. There are two models in solution which are developed based on the structure of JSon.






