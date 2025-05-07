Hello there! :)

This is a quick example for a logistic workflow setup to remove some Items that are on StockKeepingUnits in a warehouse.
This example can be split up into 3 parts: 
  - The Entities represent the different Entities used in the Warehouse. They are simplified for this example.
  - The Workflow represents the workflow we try to achieve itself. Normally I would recommend to split the Methods into their own classes to make a more modular approach and possible additional usage in other workflows possible as well, but that would have been beyond the scope of the example.
  - The DbContext and Program.cs that provide together with EntityFramework the Database and backbone. In this case a InMemoryDataBase is used and the given Entities are translated into the DB automatically. This is also the reason why the Entity setup represents the DB Entity setup directly.
    I put some mockup data into the Programm via the DBContext, but as there is no UI or anything this is just for demonstration purposes and to show how to add some Data into the example (as well as access it in the Workflow class itself).
In General this whole setup could be improved and extended in many ways, like Options or even a real way to determine a good workflow, but this would be beyond the scope as well.
If you have any questions, just let me know and contact me :)

Have a nice day! 
