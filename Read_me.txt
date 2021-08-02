This sample application is for uploading large file on the server using dot net web api and react js web application.

Instruction to run the application

Run the dot net core web api first (It will be running on https://localhost:44356/fileupload). Once, it starts running, it will display the response message

Open and Run React web application (http://localhost:3000/)

Browse csv file, the data will be saved on the server and it will display the progressbar while uploading file.

Currently, the target path is configured in the directory "C:\\TargetFolder" on the server.

There is api SaveJsonData which reads the latest csv file from the directory and saves the data in json file in the configure directory "C:\\TargetFolder\\artikel.json"

To Save json data, call the webapi https://localhost:44356/jsonconverter/SaveJson

To Save the data in SQL Server, Invoke the webapi https://localhost:44356/jsonconverter/SaveData 

Note: To save the data in sql server database, we need to update the server details in the appsettings.json file. This is the sample application. There are only few validation and error handling. 
It will working fine if all the configuration is correct.

Please reach me on email : ajay9271@gmail.com for any clarification. Any comments or suggestions are always welcome.