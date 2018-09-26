import java.io.File;
import java.io.FileNotFoundException;
import java.util.Scanner;

/**
 * A test driver for the included Cache class.
 * 
 * @author Konnor Collins
 * @since 9/5/2017
 */
public class Test {

	public static void main(String[] args) {
		// Check for proper number of arguments
		if (args.length < 3 || args.length > 4) {
			System.out.println("Usage: Test [1 | 2] [L1 cache size] [ | L2 cache size] [filename]");
			return;
		}

		// Check for valid number of caches
		int numCaches = Integer.parseInt(args[0]);

		if (numCaches != 1 && numCaches != 2) {
			System.out.println("Please specify only [1] or [2] caches.  Other inputs are invalid.");
		}

		// ONE CACHE
		if (numCaches == 1) {
			int l1size = Integer.parseInt(args[1]);
			if (l1size <= 0) {
				System.out.println(
						"Please enter a size greater than 0, otherwise this Cache will be practically useless.");
				return;
			}

			Cache<String> L1 = new Cache<String>(l1size);
			System.out.println("L1 Cache with " + l1size + " entries created");
			File file = new File(args[2]);
			try {
				Scanner in = new Scanner(file);

				// Read in from file, test cache with each individual token
				while (in.hasNext()) {
					String token = in.next();
					if (L1.get(token) == null) {
						L1.add(token);
					}
				}
				in.close();
			} catch (FileNotFoundException e) {
				e.printStackTrace();
				System.out.println("ERROR: Could not find your specified file! Aborting...");
				return;
			}

			// Stats calculation & printing to console
			int hitCount = (int) L1.getHitCount();
			int accessCount = (int) L1.getAccessCount();
			double hitRate = L1.getHitRate();

			System.out.println("Number of L1 Cache hits: " + hitCount);
			System.out.printf("L1 Cache hit rate: %.2f\n\n", hitRate * 100.0);

			System.out.println("Total number of accesses: " + accessCount);
			System.out.println("Total number of hits: " + hitCount);
			System.out.printf("Overall hit rate: %.2f\n" + hitRate * 100.0);

			return;
		}

		// TWO CACHE
		if (numCaches == 2) {
			int l1size = Integer.parseInt(args[1]);
			int l2size = Integer.parseInt(args[2]);
			if (l1size <= 0 || l2size <= 0 || l2size < l1size) { // Invalid arguments
				System.out.println(
						"Please enter a size greater than 0 for your caches.  Also ensure that your specified l1 cache size is smaller than your specified l2 cache size.");
				return;
			}

			Cache<String> L1 = new Cache<String>(l1size);

			System.out.println("L1 Cache with " + l1size + " entries created");
			System.out.println("L2 Cache with " + l2size + " entries created");

			Cache<String> L2 = new Cache<String>(l2size);
			File file = new File(args[3]);
			
			// reading in from file to test caches
			try {
				Scanner in = new Scanner(file);

				while (in.hasNext()) {
					String token = in.next();
					if (L1.get(token) == null) { // NOT FOUND L1

						if (L2.get(token) == null) { // NOT FOUND L2
							L1.add(token);
							L2.add(token);
						} else { // FOUND L2
							L1.add(token);
						}
					} else {
						L2.write(token); // push L2 entry to top
					}
				}
				in.close();
			} catch (FileNotFoundException e) {
				e.printStackTrace();
				System.out.println("ERROR: Could not find your specified file! Aborting...");
				return;
			}

			// Stat calculations & printing to console
			int l1HitCount = (int) L1.getHitCount();
			int l2HitCount = (int) L2.getHitCount();
			int l1AccessCount = (int) L1.getAccessCount();
			int l2AccessCount = (int) L2.getAccessCount();
			double l1HitRate = L1.getHitRate();
			double l2HitRate = L2.getHitRate();

			double overallHitRate = (L1.getHitCount() + L2.getHitCount()) / L1.getAccessCount();

			System.out.println("Number of L1 Cache hits: " + l1HitCount);
			System.out.printf("L1 Cache hit rate: %.2f\n\n", l1HitRate * 100.0);

			System.out.println("Number of L2 Cache hits: " + l2HitCount);
			System.out.printf("L2 Cache hit rate: %.2f\n\n", l2HitRate * 100.0);
			
			System.out.println("Total number of accesses: " + (l1AccessCount + l2AccessCount));
			System.out.println("Total number of hits: " + (l1HitCount + l2HitCount));
			System.out.printf("Overall hit rate: %.2f", overallHitRate);

			return;
		}
	}
}
