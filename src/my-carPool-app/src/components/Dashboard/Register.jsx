function Register() {
  /* // Check if overlap exists between the new opportunity and existing opportunity
function checkOverlap(newDeparture, newArrival, existingOpportunities) {
    for (each opportunity in existingOpportunities) {
      if (newDeparture is between opportunity's departure and arrival 
          or newArrival is between opportunity's departure and arrival) {
        return true
      }
    }
    return false
  }
  
  // On submit button click
  submitButton.onClick(function() {
    newDeparture = getDepartureTime()
    newArrival = getArrivalTime()
    existingOpportunities = getExistingOpportunities()
  
    if (checkOverlap(newDeparture, newArrival, existingOpportunities)) {
      showError("An opportunity already exists during this time. Please choose a different time.")
    } else {
      submitForm()
    }
  })

  // Store each Oppertunity inside a database

*/

  return (
    <form class="w-full max-w-sm mx-auto mt-10 h-full">
      <div class="mb-6">
        <label
          class="block text-gray-700 font-medium mb-2"
          for="departure-time"
        >
          Departure time
        </label>
        <input
          class="w-full border border-gray-400 rounded-lg py-2 px-4 focus:outline-none focus:border-green-400"
          id="departure-time"
          type="time"
          required
        />
      </div>
      <div class="mb-6">
        <label class="block text-gray-700 font-medium mb-2" for="arrival-time">
          Expected Arrival time
        </label>
        <input
          class="w-full border border-gray-400 rounded-lg py-2 px-4 focus:outline-none focus:border-green-400"
          id="arrival-time"
          type="time"
          required
        />
      </div>
      <div class="mb-6">
        <label class="block text-gray-700 font-medium mb-2" for="origin">
          Origin
        </label>
        <input
          class="w-full border border-gray-400 rounded-lg py-2 px-4 focus:outline-none focus:border-green-400"
          id="origin"
          type="text"
          required
        />
      </div>
      <div class="mb-6">
        <label
          class="block text-gray-700 font-medium mb-2"
          for="days-available"
        >
          Days available
        </label>
        <input
          class="w-full border border-gray-400 rounded-lg py-2 px-4 focus:outline-none focus:border-green-400"
          id="days-available"
          type="text"
        />
      </div>
      <div class="mb-6">
        <label class="block text-gray-700 font-medium mb-2" for="destination">
          Destination
        </label>
        <input
          class="w-full border border-gray-400 rounded-lg py-2 px-4 focus:outline-none focus:border-green-400"
          id="destination"
          type="text"
          required
        />
      </div>
      <div class="mb-6">
        <label class="block text-gray-700 font-medium mb-2" for="seats">
          Available seats
        </label>
        <input
          class="w-full border border-gray-400 rounded-lg py-2 px-4 focus:outline-none focus:border-green-400"
          id="seats"
          type="number"
          required
          min="1"
          max="4"
        />
      </div>
      <div class="mb-6">
        <label class="block text-gray-700 font-medium mb-2" for="owner">
          Owner/Leader
        </label>
        <input
          class="w-full border border-gray-400 rounded-lg py-2 px-4 focus:outline-none focus:border-green-400"
          id="owner"
          type="text"
          required
        />
      </div>
      <div class="mb-6">
        <label class="block text-gray-700 font-medium mb-2" for="notes">
          Notes
        </label>
        <textarea
          class="w-full border border-gray-400 rounded-lg py-2 px-4 focus:outline-none focus:border-green-400"
          id="notes"
        ></textarea>
      </div>
      <div class="flex items-center justify-end mt-4 mb-4">
        <button
          class="bg-green-400 hover:bg-green-600 text-black font-medium py-2 px-4 rounded-lg focus:outline-none focus:shadow-outline"
          type="submit"
        >
          Submit
        </button>
      </div>
    </form>
  );
}

export default Register;
