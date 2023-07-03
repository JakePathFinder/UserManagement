# UserManagement

The User management solution provides APIs for managing users.
The solution contains a docker-compose.yml file that pulls and populates a mysql db
![image](https://github.com/JakePathFinder/UserManagement/assets/59265424/6ab196c0-872d-4b66-acec-3a29421640f9)
Controllers -> Contain the User Controller and the BaseEntityController -> For Future Expansion
![image](https://github.com/JakePathFinder/UserManagement/assets/59265424/1e61d778-6c95-4d8d-aca4-6dda44a94353)

Services -> Contain the User Service, the BaseEntityService (For Future Expansion)
As well as the SecurityService, used for hashing and salting the password
![image](https://github.com/JakePathFinder/UserManagement/assets/59265424/57caed3b-90ca-4e6d-b01f-c580c0a7a6f3)

Repos -> Used for data persistancy. Contain a DAL interface implemented using the MySqlRepoBase (Also used for future expansion)
Contains the UserRepo
![image](https://github.com/JakePathFinder/UserManagement/assets/59265424/2b891b99-7bfe-4a6a-94e9-1c1673b73099)

Additional Folders:
DTO -> Data Transfer objects
Model -> Contains the domain models, decoupled from the API requests\responses
Example usage: The DTO.CreateUserRequest gets a password, Maps to Model.User for saving
  The retrieved response is than mapped to a DTO.USerResponse which is returned to the user (without the password for security)
Mapping -> Mapping Data
Middleware -> Additional middleware to Log Incoming\Outgoing requests
CFG -> Configuration representation
Const -> Constants
Scripts -> Additional scripts such as creating the DB (loaded automatically using docker-compose)
Utilities -> Additional utilities, currently storing FileHelper, used to read the .csv file in batches

Upon running The followinf Swagger API shall be opened via the browser:
![image](https://github.com/JakePathFinder/UserManagement/assets/59265424/03e81449-3952-42bc-be18-65c2688a1c48)

To run the api, please use the "Try it out" button:
![image](https://github.com/JakePathFinder/UserManagement/assets/59265424/b5ce184d-432a-4353-a956-9ab46ebc67a2)

Example api usage:
1. Get all -> Used for demonstrating. No entitites exists initially:
It returns no items and the response code is 200:
![image](https://github.com/JakePathFinder/UserManagement/assets/59265424/296094ee-0efe-4dc6-9a3e-96812964d0d3)

Assuming the content of the followinf .csv file:
![image](https://github.com/JakePathFinder/UserManagement/assets/59265424/06acf617-cd37-403a-9042-a74bea8fc2c1)

2. Bulk Create:
Select the method (Create), File and press "Execute"
![image](https://github.com/JakePathFinder/UserManagement/assets/59265424/55796c34-c667-4482-a34e-ab9736412466)
The response shall include all the above entities:
![image](https://github.com/JakePathFinder/UserManagement/assets/59265424/9830197c-af56-4949-8f0b-600c07a64ed2)
(Scrolled view of the 2nd hald of the response:)
![image](https://github.com/JakePathFinder/UserManagement/assets/59265424/12952191-3185-4ade-9aa0-64e7b619efb5)
Evidently, it contains a summary of the total requests as well as the distribution to Successful\Unsucessful requests

3. Updating:
Open the Update (Put) Api and insert the user details.
Partial updates are also allowed:
![image](https://github.com/JakePathFinder/UserManagement/assets/59265424/7c13390e-e07b-4880-8f17-334cfc5f15f3)
Result:
![image](https://github.com/JakePathFinder/UserManagement/assets/59265424/4e6f71f6-4b54-4af4-9730-4479b7bf82f8)

4. Deleting:
   Simply insert the Id:
   ![image](https://github.com/JakePathFinder/UserManagement/assets/59265424/d13b31a0-495a-4082-a191-fff89deebc7a)

5. Get By Id:
   Simply insert the Id (We can see that changed Jane Doe)
   ![image](https://github.com/JakePathFinder/UserManagement/assets/59265424/3f91ef8e-6189-40e7-b516-dfaa4d058866)
   














