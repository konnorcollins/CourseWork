import java.io.File;
import java.io.FileNotFoundException;
import java.util.Hashtable;
import java.util.Scanner;

/**
 * Sister simulation to HashTest.java, but uses Java's HashTable implementation,
 * which uses chaining.
 * 
 * @author Konnor Collins
 * @since 10-19-2017
 */
public class HashTestWithJava {

	/**
	 * Reads in tokens of a specified type from a given file. Each token is
	 * individually inserted into a HashTable.
	 * 
	 * @param args
	 */
	public static void main(String[] args) {

		// ARGS PROCESSING
		if (args.length < 4 || args.length > 5) {
			System.out.println(
					"Usage: java HashTestWithJava [input type] [load factor] [hash table capacity] [input file] [ |debug level]");
			return;
		}

		int inputType = Integer.parseInt(args[0]);
		if (inputType < 0 || inputType > 2) {
			throw new IllegalArgumentException(
					"Invalid input type. [0]: Integer values. [1]: Long values. [2] String values.");
		}

		float loadFactor = Float.parseFloat(args[1]);
		if (loadFactor < 0 || loadFactor > 1) {
			throw new IllegalArgumentException("Invalid load factor.  Please reconsider.");
		}

		int hashTableCapacity = Integer.parseInt(args[2]);
		if (hashTableCapacity < 13)
			hashTableCapacity = 13;

		String fileName = args[3];

		int debugLevel = 0;
		if (args.length == 5) {
			debugLevel = Integer.parseInt(args[4]);
		}

		int maxSize = (int) ((double) hashTableCapacity * (double) loadFactor);

		// SIMULATION
		try {
			Scanner in = new Scanner(new File(fileName));

			int dupeCount = 0;

			switch (inputType) {

			case 0: // INTEGER
			{
				Hashtable<Integer, Integer> javaTable = new Hashtable<Integer, Integer>(hashTableCapacity, loadFactor);
				// running
				while (in.hasNextInt() && !(javaTable.size() >= maxSize)) {
					Integer e = in.nextInt();
					Integer k = Math.abs(e.hashCode());
					if (javaTable.containsKey(k)) {
						dupeCount++;
					}
					javaTable.put(k, e);
				}

				// print info
				System.out.println("Table size: " + hashTableCapacity);
				System.out.println("Data source: " + fileName);
				System.out.println("Data type: Integer");
				System.out.println("Using Java Hashtable...");
				System.out.println(
						"Inserted " + javaTable.size() + " elements, of which " + dupeCount + " are duplicates\n");
				System.out.println("Load Factor: " + loadFactor);

				if (debugLevel == 1) {
					System.out.println(javaTable);
				}
			}
				break;

			case 1: // LONG
			{
				Hashtable<Integer, Long> javaTable = new Hashtable<Integer, Long>(hashTableCapacity, loadFactor);

				// running
				while (in.hasNextLong() && !(javaTable.size() >= maxSize)) {
					Long e = in.nextLong();
					Integer k = Math.abs(e.hashCode());
					if (javaTable.containsKey(k)) {
						dupeCount++;
					}
					javaTable.put(k, e);
				}

				// print info
				System.out.println("Table size: " + hashTableCapacity);
				System.out.println("Data source: " + fileName);
				System.out.println("Data type: Long");
				System.out.println("Using Java Hashtable...");
				System.out.println(
						"Inserted " + javaTable.size() + " elements, of which " + dupeCount + " are duplicates\n");
				System.out.println("Load Factor: " + loadFactor);

				if (debugLevel == 1) {
					System.out.println(javaTable);
				}
			}
				break;

			case 2: // DOUBLE
			{
				Hashtable<Integer, String> javaTable = new Hashtable<Integer, String>(hashTableCapacity, loadFactor);
				// running
				while (in.hasNext() && !(javaTable.size() >= maxSize)) {
					String e = in.next();
					Integer k = Math.abs(e.hashCode());
					javaTable.put(k, e);
					if (javaTable.containsKey(k)) {
						dupeCount++;
					}
					javaTable.put(k, e);
				}

				// print info
				System.out.println("Table size: " + hashTableCapacity);
				System.out.println("Data source: " + fileName);
				System.out.println("Data type: String");
				System.out.println("Using Java Hashtable...");
				System.out.println(
						"Inserted " + javaTable.size() + " elements, of which " + dupeCount + " are duplicates\n");
				System.out.println("Load Factor: " + loadFactor);

				if (debugLevel == 1) {
					System.out.println(javaTable);
				}
			}
				break;
			}

			in.close();
		} catch (FileNotFoundException e) {
			e.printStackTrace();
		}

	}

}
