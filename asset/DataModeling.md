## Scenario analysis
In the real world, a customer sit in the restaurant, a digital menu will be provided to her/him, or a customer 
want to know dishes information before she/he go to a restaurant. Such scenario implicit a precondition, the dishes 
are aggregated to a restaurant. 

Follow the MongoDB data modeling pattern, the data model should follow the application request. Therefore I assume 
- A customer read a menu of a restaurant as one scenario. 
- The data owner's operation of restaurant/dishes from the other side is another
scenario. 

## Scenario goal
- Customer could query menu information when she/he want to have dishes information of a restaurant
- Restaurant could insert/update/query dishes information

## Workload

### List of operations
| Query | Operation | Description |
|:---|:---:|:---|
| *Restaurant operations* |
| Create restaurant | write  | User create a new restaurant |
| Update restaurant | write  | User change properties of restaurant |
| Read restaurant | read  | User query restaurant by restaurant id |
| Delete restaurant | write  | User delete restaurant by restaurant id |
| *Dishes operations* |
| Create dishes | write  | User create a new dishes |
| Update dishes | write  | User change properties of dishes |
| Read dishes | read  | User query dishes by dish id |
| Delete dishes | write  | User delete dishes by dish id |
| *Menu operations* |
| Get dishes information of a restaurant|read| Use query menu which include restaurant and dishes information|

### Quantify and qualify the operations
This part is only reflection the real data model design, useful data volume diagnose, creating index etc. But in 
this application is not really useful.

Because there is no real data to support, the numbers base on total hotel numbers in Germany, and assume each hotel has
`1` restaurant, each restaurant has `20` dishes. Base on [de.statista.com](https://de.statista.com/themen/2639/beherbergungsgewerbe-in-deutschland/#:~:text=Der%20Umsatz%20im%20Beherbergungsgewerbe%20in,und%20ausl%C3%A4ndischen%20G%C3%A4sten%20get%C3%A4tigt%20wurden.)
data, there are `51, 200` geöffnete Beherbergungsbetriebe in Germany 2019, we assume:

- All dishes data in one database, the total dishes will be `1,024,000`
- Each day `10%` of total dishes will be created or migrated from exist RDBMS or from UI/API
- `20%` of dishes active/inactive/awaiting time will be updated
- Each restaurant has avg.`20` customers per day, who will use the UI/API to query the dishes
- `10` customer after query the dishes will change the filters for getting mor information

| Query | Quantification | Qualification |
|:---|:---:|:---|
| Create new restaurant | `5,120` write/day<br> each write < 5 sec | critical write |
| Update restaurant | `5,120` write/day<br> each write < 5 sec | critical write |
| Create new dishes | `102,400` write/day<br> each write < 5 sec | critical write |
| Update dishes properties |`102,400` write/day<br>each write < 5 sec | critical write |
| Update dishes active/inactive/awaiting time|`204,800` write/day<br>each write < 1 sec | critical write |
| Query restaurant by restaurant id | `1,024,000` read/day<br> each read < 5 ms | no stale data |
| Query dishes by restaurant id | `1,024,000` read/day<br> each read < 5 ms | no stale data |
| Query dishes by dishes id| `10,240,000` read/day<br> each read < 5 ms | no stale data |

### Entities
For dishes themself, a simplicity all embedded model is enough, dish should be link to restaurant
The relationship between dish and restaurant, the image also include the user role:
![dishes and restaurant](DishesAndRestaurant.png?raw=true)

 ### Patterns
The restaurant id in dish is kind of *Extended reference* pattern, but not really, because the model is 
every simple, the pattern in this case might even not needed. 

### Mongo db data modeling vs. MSSQL data modeling
1. Mongo db data modeling rely on application request, normalization are not needed. On the other side - 
   MSSQL (RDBMS), the data could be independent application request, normalization is needed.
2. When application request changed, Mongo db could more easier change the data model and persistence, RDBMS 
need more work to fit the application request change
For instance, in this e.g. when a new properties of dishes or restaurant will be appended, we just need fix the
   application side, the new appended properties will be simply saved in the db, we could also just fix the application 
   side for data versioning.
   
### The power of digital menu
There are some benefit we could sell to our customer (Restaurant owner):
1. Dishes can be shown via AR tech, better than just pictures, improve dishes order rate
2. Restaurant can easily change the awaiting time
3. Restaurant could easy maintenance their dishes information, print-free 
4. Restaurant customer could use digital menu order dishes, the menu usage data analysis could be easier 
   saved and analysis result could help restaurant business
5. More and more restaurant will use digital menu, restaurant can gather the data about the dishes order
   (no person data, but dishes data and order data - time, how often, price, etc.), 
   the data is valuable for whole branch research
6. Restaurant could use the gathered data to avoid partial software service cost 
