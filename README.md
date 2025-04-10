# SQL Query Processing Interface - C# Windows Forms Application

This is a C# Windows Forms application that allows users to execute SQL queries via a graphical interface and it is a part of university course work. The application supports SQL operations such as **CREATE TABLE**, **DROP TABLE**, **DELETE**, **GET**, **SELECT**, **TABLE INFO**, and **INSERT**, and works with tables loaded from the `data.txt` and `meta.txt` files.

The application is designed for easy interaction with a database and provides a convenient way to execute SQL queries and manage tables.

## Key Features:
- **Execute SQL Queries**: The application allows executing SQL queries, including:
  - **CREATE TABLE**: Create new tables.
  - **DROP TABLE**: Delete an existing table.
  - **INSERT**: Add new records to a table.
  - **DELETE**: Remove records from a table.
  - **SELECT / GET**: Retrieve data from a table.
- **Graphical User Interface**: A user-friendly interface for entering SQL queries, executing them, and viewing results.
- **Result Processing**: The results of the queries are displayed either in a ListView.

## Technologies Used:
- **C#**: Programming language for implementing the application logic.
- **Windows Forms**: Framework for creating the graphical user interface.
- **.NET Framework**: The underlying framework supporting Windows Forms applications.
- **Text Files (`data.txt`, `meta.txt`)**: Used for storing table data and metadata.

## Installation

1. Clone or download the repository to your local machine.
2. Open the project in **Visual Studio** (make sure you have the required .NET Framework version installed).
3. Build and run the project from Visual Studio.

## How to Use:

1. **Enter a Query**: Type an SQL query in the text box.
2. **Execute the Query**: Click the "Execute" button to run the query.
3. **View Results**: The results of the query will be displayed in a **ListView**.
5. **Working with `data.txt` and `meta.txt` files**:
   - **data.txt** contains data for the tables.
   - **meta.txt** contains metadata for the table structure, such as column names and data types.

Created by Iva Boneva
University: Technical University of Sofia
Course: Synthesis and analysis of algorithms
Year: 2024
