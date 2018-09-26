import java.io.File;
import java.io.FileNotFoundException;
import java.text.DecimalFormat;
import java.util.Scanner;

/**
 * A basic testing command line suite for home-made HashTable and HashObject
 * classes.
 * 
 * @author Konnor Collins
 * @since 10-18-2017
 */
public class HashTest {

	/**
	 * Reads and scans input tokens from a specified file. These tokens are then
	 * stored in HashObjects and assigned a key. <br />
	 * <br />
	 * The HashObjects are then inserted into both a linear-probe HashTable and
	 * a double-hash HashTable. <br />
	 * <br />
	 * If the fifth flag (debug level) is set to 1, each table's contents will
	 * be printed after the simulation has been run. After
	 * 
	 * @param args
	 */
	public static void main(String[] args) {
		// ARGS PROCESSING
		if (args.length < 4 || args.length > 5) {
			System.out.println(
					"Usage: java HashTest [load factor] [hash table capacity] [input file] [input type] [ | debug level]");
			return;
		}

		float loadFactor = Float.parseFloat(args[0]);
		if (loadFactor < 0 || loadFactor > 1) {
			throw new IllegalArgumentException("Invalid load factor.  Please reconsider.");
		}
		int hashTableCapacity = Integer.parseInt(args[1]);
		if (hashTableCapacity < 13)
			hashTableCapacity = 13;
		String fileName = args[2];

		int inputType = Integer.parseInt(args[3]);
		if (inputType < 0 || inputType > 2) {
			throw new IllegalArgumentException(
					"Invalid input type. [0]: Integer values. [1]: Long values. [2] String values.");
		}

		int debugLevel = 0;
		if (args.length == 5) {
			debugLevel = Integer.parseInt(args[4]);
			if (debugLevel < 0 || debugLevel > 1) {
				throw new IllegalArgumentException(
						"Invalid debug level. [0]: Summary.  [1]: Summary + Contents/Frequency.");
			}
		}

		// SIMULATION
		try {
			Scanner in = new Scanner(new File(fileName));
			DecimalFormat df = new DecimalFormat(".##");

			switch (inputType) {
			case 0: { // INTEGERS
				HashTable<Integer> linearTable = new HashTable<Integer>(hashTableCapacity, loadFactor,
						OpenAddressType.linear);
				HashTable<Integer> doubleHashTable = new HashTable<Integer>(hashTableCapacity, loadFactor,
						OpenAddressType.doubleHashing);

				// run simulation
				while (in.hasNextInt() && !linearTable.isFull() && !doubleHashTable.isFull()) {
					Integer e = in.nextInt();
					int key = Math.abs(e.hashCode());
					linearTable.put(e, key);
					doubleHashTable.put(e, key);
				}

				// calculate averages
				int linearElements = linearTable.size();
				int linearProbes = linearTable.getNumProbes();
				double linearProbesAverage = (double) linearProbes / (double) linearElements;

				int doubleHashElements = doubleHashTable.size();
				int doubleHashProbes = doubleHashTable.getNumProbes();
				double doubleHashProbesAverage = (double) doubleHashProbes / (double) doubleHashElements;

				// print info
				System.out.println("Using Linear Probing...");
				System.out.println(
						"Inserted " + linearElements + " values with " + linearTable.getDupeCount() + " duplicates");
				System.out.println("Load factor: " + linearTable.getLoadFactor() + " Average number of probes: "
						+ df.format(linearProbesAverage) + "\n");

				if (debugLevel == 1) {
					System.out.println(linearTable.toString() + "\n");
				}

				System.out.println("Using Double Hash Probing...");
				System.out.println("Inserted " + doubleHashElements + " values with " + doubleHashTable.getDupeCount()
						+ " duplicates");
				System.out.println("Load factor: " + doubleHashTable.getLoadFactor() + " Average number of probes: "
						+ df.format(doubleHashProbesAverage) + "\n");

				if (debugLevel == 1) {
					System.out.println(doubleHashTable.toString() + "\n");
				}

			}
				break;

			case 1: { // LONGS
				HashTable<Long> linearTable = new HashTable<Long>(hashTableCapacity, loadFactor,
						OpenAddressType.linear);
				HashTable<Long> doubleHashTable = new HashTable<Long>(hashTableCapacity, loadFactor,
						OpenAddressType.doubleHashing);

				// run simulation
				while (in.hasNextLong() && !linearTable.isFull() && !doubleHashTable.isFull()) {
					Long e = in.nextLong();
					int key = Math.abs(e.hashCode());
					linearTable.put(e, key);
					doubleHashTable.put(e, key);
				}

				// calculate averages
				int linearElements = linearTable.size();
				int linearProbes = linearTable.getNumProbes();
				double linearProbesAverage = (double) linearProbes / (double) linearElements;

				int doubleHashElements = doubleHashTable.size();
				int doubleHashProbes = doubleHashTable.getNumProbes();
				double doubleHashProbesAverage = (double) doubleHashProbes / (double) doubleHashElements;

				// print info
				System.out.println("Using Linear Probing...");
				System.out.println(
						"Inserted " + linearElements + " values with " + linearTable.getDupeCount() + " duplicates");
				System.out.println("Load factor: " + linearTable.getLoadFactor() + " Average number of probes: "
						+ df.format(linearProbesAverage) + "\n");

				if (debugLevel == 1) {
					System.out.println(linearTable.toString() + "\n");
				}

				System.out.println("Using Double Hash Probing...");
				System.out.println("Inserted " + doubleHashElements + " values with " + doubleHashTable.getDupeCount()
						+ " duplicates");
				System.out.println("Load factor: " + doubleHashTable.getLoadFactor() + " Average number of probes: "
						+ df.format(doubleHashProbesAverage) + "\n");

				if (debugLevel == 1) {
					System.out.println(doubleHashTable.toString() + "\n");
				}

			}
				break;

			case 2: {// STRINGS
				HashTable<String> linearTable = new HashTable<String>(hashTableCapacity, loadFactor,
						OpenAddressType.linear);
				HashTable<String> doubleHashTable = new HashTable<String>(hashTableCapacity, loadFactor,
						OpenAddressType.doubleHashing);

				// run simulation
				while (in.hasNext() && !linearTable.isFull() && !doubleHashTable.isFull()) {
					String e = in.next();
					int key = Math.abs(e.hashCode());
					linearTable.put(e, key);
					doubleHashTable.put(e, key);
				}

				// calculate averages
				int linearElements = linearTable.size();
				int linearProbes = linearTable.getNumProbes();
				double linearProbesAverage = (double) linearProbes / (double) linearElements;

				int doubleHashElements = doubleHashTable.size();
				int doubleHashProbes = doubleHashTable.getNumProbes();
				double doubleHashProbesAverage = (double) doubleHashProbes / (double) doubleHashElements;

				// print info
				System.out.println("Using Linear Probing...");
				System.out.println(
						"Inserted " + linearElements + " values with " + linearTable.getDupeCount() + " duplicates");
				System.out.println("Load factor: " + linearTable.getLoadFactor() + " Average number of probes: "
						+ df.format(linearProbesAverage) + "\n");

				if (debugLevel == 1) {
					System.out.println(linearTable.toString() + "\n");
				}

				System.out.println("Using Double Hash Probing...");
				System.out.println("Inserted " + doubleHashElements + " values with " + doubleHashTable.getDupeCount()
						+ " duplicates");
				System.out.println("Load factor: " + doubleHashTable.getLoadFactor() + " Average number of probes: "
						+ df.format(doubleHashProbesAverage) + "\n");

				if (debugLevel == 1) {
					System.out.println(doubleHashTable.toString() + "\n");
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
