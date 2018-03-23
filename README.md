# cbGenerateTableTest
Test and Design environment for a table generator, coinciding with the scatterplot generator class.


## Building and running the test program

1. Open the solution in an IDE.
2. Open Form1/frmGenerateTableTestForm and navigate to the btnGenerateBasicTable/button1_Click() method's C# code.
3. Provide custom maximumYValue and numberOfDatapoints, and optionally a tableFileName, and header names.
4. Run the windows forms application and click the Generate Basic Table button to create a new table file in the bin/debug or release folder.

## Limitations

Integer values only: The program is designed to work alongside algorithm tests - the X-values are meant to be the N value for a list, and the Y values are meant to be the number of operations required to sort that list.  Therefore, the tables are generated with the assumption that all input values are integers.
