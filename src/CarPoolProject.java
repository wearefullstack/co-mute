import java.util.*;
import java.text.*;
import java.io.*;
import java.sql.*;

public class CarPoolProject {

	public static void main(String[] args) {
		
		System.out.println("Welcome to the Co-Mute!!! \n");   // Welcoming message for the user
        while (true) {
            System.out.println("\nPlease choose a number option from the menu below: "
                    + "\n1. Register new user"
                    + "\n2. Register new car-pool"
                    + "\n3. Leave/join car-pool"
                    + "\n4. Travel opportunities"
                    + "\n5. Find car-pool opportunities"
                    + "\n6. Update details"
                    + "\n7. Exit");   // Options for the user to choose from

                    Scanner scanner = new Scanner(System.in);
                    int choice = scanner.nextInt();
                    
                    try {
                        Connection connection = DriverManager.getConnection(
                                "jdbc:mysql://127.0.0.1:3306/carPool",
                                "root",
                                "Phumuboi1996");
                        
                        // Create a direct line to the database for running our queries.
                        Statement statement = connection.createStatement();
                        ResultSet results;
                        int rowsAffected;

                    if(choice == 1){  // adding new users 
                    	
                        Scanner scanner1 = new Scanner(System.in);
            		    System.out.println("\nPlease enter the user id: ");
                        int userId = scanner1.nextInt();

                        Scanner scanner2 = new Scanner(System.in);
                        System.out.println("Please enter the user's name: ");
                        String name = scanner2.nextLine();

                        Scanner scanner3 = new Scanner(System.in);
                        System.out.println("Please enter the user's surname: ");
                        String surname = scanner3.nextLine();

                        Scanner scanner4 = new Scanner(System.in);
                        System.out.println("Please enter the user's phone number: ");
                        int phoneNum = scanner4.nextInt();

                        Scanner scanner5 = new Scanner(System.in);
                        System.out.println("Please enter the user's email address: ");
                        String email = scanner5.nextLine();

                        Scanner scanner6 = new Scanner(System.in);
                        System.out.println("Please enter the user's password: ");
                        String password = scanner6.nextLine();

                        String sql = "INSERT INTO user" + "(user_id, name, surname, phone, email, password)"+
                        "VALUES (?, ?, ?, ?, ?, ?)";
                        PreparedStatement myStmt = connection.prepareStatement(sql);
                        myStmt.setInt(1, userId);
                        myStmt.setString(2, name);
                        myStmt.setString(3, surname);
                        myStmt.setInt(4, phoneNum);
                        myStmt.setString(5, email);
                        myStmt.setString(6, password);
                        myStmt.executeUpdate();
                        printAllFromTable(myStmt);
                    
                    }else if(choice ==2){ // creating a new car-pool opportunity
                    	
                        Scanner scanner7 = new Scanner(System.in);
            		    System.out.println("\nPlease enter the car-pool id: ");
                        int cPoolId = scanner7.nextInt();

                        Scanner scanner8 = new Scanner(System.in);
                        System.out.println("Please enter departure time(eg.0900/1630): ");
                        int dTime = scanner8.nextInt();

                        Scanner scanner9 = new Scanner(System.in);
                        System.out.println("Please enter arrival time(eg.0900/1630): ");
                        int aTime = scanner9.nextInt();

                        Scanner scanner10 = new Scanner(System.in);
                        System.out.println("Please enter origin: ");
                        String origin = scanner10.nextLine();

                        Scanner scanner11 = new Scanner(System.in);
                        System.out.println("Please enter the number of avaliable days: ");
                        int aDays = scanner11.nextInt();

                        Scanner scanner12 = new Scanner(System.in);
                        System.out.println("Please enter destination: ");
                        String destination = scanner12.nextLine();

                        Scanner scanner13 = new Scanner(System.in);
                        System.out.println("Please enter avaliable seats: ");
                        int seats = scanner13.nextInt();

                        Scanner scanner14 = new Scanner(System.in);
                        System.out.println("Please enter owner: ");
                        String owner = scanner14.nextLine();

                        String sql = "INSERT INTO carpool_opportunity" + "(carpool_id, departure_time, arrival_time, origin, days_avaliable, destination, avaliable_seats, owner)"+
                        "VALUES (?, ?, ?, ?, ?, ?, ?, ?)";
                        PreparedStatement myStmt = connection.prepareStatement(sql);
                        myStmt.setInt(1, cPoolId);
                        myStmt.setInt(2, dTime);
                        myStmt.setInt(3, aTime);
                        myStmt.setString(4, origin);
                        myStmt.setInt(5, aDays);
                        myStmt.setString(6, destination);
                        myStmt.setInt(7, seats);
                        myStmt.setString(8, owner);
                        myStmt.executeUpdate();
                        printAllFromTable1(myStmt);

                    }else if(choice == 3){   // joining or deleting a car-pool entry

                        Scanner scanner10 = new Scanner(System.in);
            		    System.out.println("Would you like to:" +
            				"\n1. Would you like to leave car-pool or" +
            				"\n2. Join car-pool?" + // Edit options displayed.
            				"\nChoose either 1 or 2");
                        int newChoice = scanner.nextInt();

                        if(newChoice==1){  // deleting a car-pool entry
                            Scanner scanner11 = new Scanner(System.in);
            		        System.out.println("Enter car-pool id you want to delete: ");
                            int cPoolId = scanner.nextInt();

                            String sql = "DELETE FROM carpool_opportunity WHERE carpool_id=?";
            			    PreparedStatement myStmt = connection.prepareStatement(sql);
            			    myStmt.setInt(1, cPoolId);
            			    myStmt.executeUpdate();
            			    printAllFromTable1(myStmt);

                        }else if(newChoice==2){  // joining a car-pool

                            Scanner scanner7 = new Scanner(System.in);
            		        System.out.println("\nPlease enter the car-pool id: ");
                            int cPoolId = scanner7.nextInt();

                            Scanner scanner8 = new Scanner(System.in);
                            System.out.println("Please enter departure time: ");
                            int dTime = scanner8.nextInt();

                            Scanner scanner9 = new Scanner(System.in);
                            System.out.println("Please enter arrival time: ");
                            int aTime = scanner9.nextInt();

                            Scanner scanner1 = new Scanner(System.in);
                            System.out.println("Please enter origin: ");
                            String origin = scanner1.nextLine();

                            Scanner scanner11 = new Scanner(System.in);
                            System.out.println("Please enter the number of avaliable days: ");
                            int aDays = scanner11.nextInt();

                            Scanner scanner12 = new Scanner(System.in);
                            System.out.println("Please enter destination: ");
                            String destination = scanner12.nextLine();

                            Scanner scanner13 = new Scanner(System.in);
                            System.out.println("Please enter avaliable seats: ");
                            int seats = scanner13.nextInt();

                            Scanner scanner14 = new Scanner(System.in);
                            System.out.println("Please enter owner: ");
                            String owner = scanner14.nextLine();

                            String sql = "INSERT INTO carpool_opportunity" + "(carpool_id, departure_time, arrival_time, origin, days_avaliable, destination, avaliable_seats, owner)"+
                            "VALUES (?, ?, ?, ?, ?, ?, ?, ?)";
                            PreparedStatement myStmt = connection.prepareStatement(sql);
                            myStmt.setInt(1, cPoolId);
                            myStmt.setInt(2, dTime);
                            myStmt.setInt(3, aTime);
                            myStmt.setString(4, origin);
                            myStmt.setInt(5, aDays);
                            myStmt.setString(6, destination);
                            myStmt.setInt(7, seats);
                            myStmt.setString(8, owner);
                            myStmt.executeUpdate();
                            printAllFromTable1(myStmt);
                        }

                    }else if(choice == 4){  // getting all the car-pool opportunities to choose from

                        ResultSet results3 = statement.executeQuery("SELECT * FROM carpool_opportunity");
				        while (results3.next()) {
				        	System.out.println(results3.getInt("carpool_id") + ", " + results3.getInt("departure_time") + ", "
							+ results3.getInt("arrival_time") + ", " + results3.getString("origin") + "," + results3.getInt("days_avaliable") +
		                    "," + results3.getString("destination") + ", " + results3.getInt("avaliable_seats") + ", " + results3.getString("owner"));
				        }

                    }else if(choice == 5){     // finding a specific car-pool
                        Scanner scanner14 = new Scanner(System.in);
				        System.out.println("Please enter the car-pool id you wish to find: ");
				        int carpool_id2 = scanner14.nextInt();

				        ResultSet results4 = statement.executeQuery("SELECT * FROM carpool_opportunity WHERE carpool_id =" + carpool_id2);
				        while (results4.next()) {
				        	System.out.println(results4.getInt("carpool_id") + ", " + results4.getInt("departure_time") + ", "
							+ results4.getInt("arrival_time") + ", " + results4.getString("origin") + "," + results4.getInt("days_avaliable") +
		                    "," + results4.getString("destination") + ", " + results4.getInt("avaliable_seats") + ", " + results4.getString("owner"));
				        }
                               
                    
                    }else if(choice == 6){    // updating user's details

                        Scanner scanner0 = new Scanner(System.in);
                        System.out.println("Enter the user id, you to updating: ");
                        int userId = scanner0.nextInt();  // the user id that's going to be updated
                        
                        Scanner scanner1 = new Scanner(System.in);
                        System.out.println("Choose from the following numbers: "
                        		+ "\n1. Update name "
                        		+ "\n2. Udate surname"
                        		+ "\n3. Update phone number"
                        		+ "\n4. Update email address"
                        		+ "\n5. Update password");
                        int editChoice2 = scanner1.nextInt();  // Different options of what can be updated
                        
                        if(editChoice2 == 1) {  // Where the name is updated
                        	
                        	Scanner scanner2 = new Scanner(System.in);
                        	System.out.println("Please enter the user's name: ");
                        	String name = scanner2.nextLine();
                        	
                        	String sql = "UPDATE user SET name=? WHERE user_id=?";
                			PreparedStatement myStmt = connection.prepareStatement(sql);
                			myStmt.setString(1, name);
                			myStmt.setInt(2, userId);
                			myStmt.executeUpdate();
                			printAllFromTable(myStmt);
                        }else if(editChoice2 == 2) {  // Where the surname is updated
                        
                        	Scanner scanner3 = new Scanner(System.in);
                        	System.out.println("Please enter the user's surname: ");
                        	String surname = scanner3.nextLine();
                        	
                        	String sql = "UPDATE user SET surname=? WHERE user_id=?";
                			PreparedStatement myStmt = connection.prepareStatement(sql);
                			myStmt.setString(1, surname);
                			myStmt.setInt(2, userId);
                			myStmt.executeUpdate();
                			printAllFromTable(myStmt);
                        }else if(editChoice2 == 3) {  // Where the phone number is updated
                        
                        	Scanner scanner4 = new Scanner(System.in);
                        	System.out.println("Please enter the user's phone number: ");
                        	int phoneNum = scanner4.nextInt();
                        	
                        	String sql = "UPDATE user SET phone=? WHERE user_id=?";
                			PreparedStatement myStmt = connection.prepareStatement(sql);
                			myStmt.setInt(1, phoneNum);
                			myStmt.setInt(2, userId);
                			myStmt.executeUpdate();
                			printAllFromTable(myStmt);
                        }else if(editChoice2 == 4) {  // Where the email is updated
                        	
                        	Scanner scanner5 = new Scanner(System.in);
                        	System.out.println("Please enter the user's email address: ");
                        	String email = scanner5.nextLine();
                        	
                        	String sql = "UPDATE user SET email=? WHERE user_id=?";
                			PreparedStatement myStmt = connection.prepareStatement(sql);
                			myStmt.setString(1, email);
                			myStmt.setInt(2, userId);
                			myStmt.executeUpdate();
                			printAllFromTable(myStmt);
                        }else if(editChoice2 == 5) {  // Where the password is updated
                        	
                        	Scanner scanner6 = new Scanner(System.in);
                        	System.out.println("Please enter the user's password: ");
                        	String password = scanner6.nextLine();

                        	String sql = "UPDATE user SET password=? WHERE user_id=?";
                			PreparedStatement myStmt = connection.prepareStatement(sql);
                			myStmt.setString(1, password);
                			myStmt.setInt(2, userId);
                			myStmt.executeUpdate();
                			printAllFromTable(myStmt);
                        }else {
                        	System.out.println("Invalid number selection entered. Try again later");
                        }
                    
                }else if(choice == 7){
                    System.out.println("Goodbye."); // Successful logout message displayed.
				    break;
                }else {     // When an invalid selection has been entered
                	System.out.println("Invalid number selection entered. Please try again");
                }
                
                statement.close();
                connection.close();
            
            	
		} catch (SQLException e) {
			// We only want to catch a SQLException
			e.printStackTrace();
		}
     }
	}

	
    public static void printAllFromTable(Statement statement) throws SQLException {
		ResultSet results = statement.executeQuery("SELECT * FROM user");
		while (results.next()) {
            System.out.println(results.getInt("user_id") + ", " + results.getString("name") + ", "
					+ results.getString("surname") + ", " + results.getInt("phone") + ", " +
					results.getString("email") + ", " + results.getString("password"));
        }
    }
    public static void printAllFromTable1(Statement statement) throws SQLException {
		ResultSet results1 = statement.executeQuery("SELECT * FROM carpool_opportunity");
		while (results1.next()) {
            System.out.println(results1.getInt("carpool_id") + ", " + results1.getInt("departure_time") + ", "
							+ results1.getInt("arrival_time") + ", " + results1.getString("origin") + "," + results1.getInt("days_avaliable") +
                            "," + results1.getString("destination") + ", " + results1.getInt("avaliable_seats") + ", " + results1.getString("owner"));
        }
    }
}
