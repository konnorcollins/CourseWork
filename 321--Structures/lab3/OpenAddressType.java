	/**
	 * An enum that defines different types of Hash Table probing.
	 * 
	 * @author Konnor Collins
	 * @since 10-16-2017
	 *
	 */
	public enum OpenAddressType {
		linear(0), quadratic(1), // not implemented for this project
		doubleHashing(2);

		OpenAddressType(int id) {
		}
	}