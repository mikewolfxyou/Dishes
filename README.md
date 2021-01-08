## Business description
Design a data model of the menu and/or the dishes.A dish should contain:
- name
- short description
- price
- category (starter, main course, dessert, beverage, ...)
- when it is available based on time of day (breakfast, dinner, lunch, weekdays/- ends)
- it should be able to deactivate a dish, for example when it’s sold out
- how long the guest approximately has to wait for the dish after they order

Create a REST API
- Save (Create/Update) dishes to the MongoDB
- Load dished from the API

## Development
- Data modeling steps - please the [data modeling document](asset/DataModeling.md)
- local run commands
1. First build dishes api image:
```shell
docker build -t dishes -f docker/dishes.Dockerfile .
```
2. Run `docker-compose`
```shell
docker-compose up
```
3. Use browser open `localhost:5000/swagger` or use [Postman](https://www.postman.com/downloads/) import [Postman Collection](asset/MongoDBDishes.postman_collection.json) in asset folder
4. First create a restaurant. After that query menu with the returned restaurant i,
   but the dishes array is still empty
5. Then create dish which belongs to the restaurant
6. Then query menu with the restaurant id, the restaurant information and dishes information will be included. 
