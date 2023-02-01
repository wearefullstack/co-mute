

function Oppertunities() {

/* // State to store the active filter tag
  const [activeFilter, setActiveFilter] = useState(null);

  // Function to handle the filter tag click
  const handleFilterClick = (filter) => {
    setActiveFilter(filter);
  };

  // Array of opportunity data that users registered
  const opportunities = [
    {
      id: 1,
      driver: 'John Doe',
      seats: 2,
      tag: 'A',
      status: 'created',
    },
    {
      id: 2,
      driver: 'Jane Doe',
      seats: 4,
      tag: 'B',
      status: 'joined',
      date: '2022-12-02',
    },
    ...
  ];*/ 
  
  return (
    <div>
      {/* <!-- Search bar --> */}
      <input
        type="text"
        className="w-80 py-2 px-4 text-gray-700 border border-gray-400 rounded-lg mb-4"
        placeholder="Search Opportunities"
      />

      {/* <!-- Filter tags --> */}
      <div className="flex justify-center mb-4">
        <button class="bg-gray-300 px-4 py-2 mr-2 rounded-lg">Joined</button>
        <button class="bg-gray-300 px-4 py-2 mr-2 rounded-lg">Created</button>
      </div>

      {/* <!-- Opportunity divs --> */}
      <div className="w-80 p-4 rounded-lg mb-4 flex justify-between bg-green-300 transition-all duration-200 ease-in-out hover:scale-110">
        <div>
          <p className="font-bold">Driver: John Doe</p>
          <p>Seats: 2 out of 4</p>
        </div>
        <button className="bg-white px-4 py-2 rounded-lg text-black font-bold">
          Join
        </button>
      </div>

      <div className="w-80 p-4 rounded-lg mb-4 flex justify-between bg-green-300 transition-all duration-200 ease-in-out hover:scale-110">
        <div>
          <p className="font-bold">Driver: Jane Doe</p>
          <p>Seats: 4 out of 4</p>
        </div>
        <button className="bg-green-700 px-4 py-2 rounded-lg font-bold text-white">
          Full
        </button>
      </div>

      <div className="w-80 p-4 rounded-lg mb-4 flex justify-between bg-green-300 transition-all duration-200 ease-in-out hover:scale-110">
        <div>
          <p className="font-bold">Driver: Alice Johnson</p>
          <p>Seats: 2 out of 4</p>
          <p>Created: 1 February 2022</p>
        </div>
        <button className="bg-green-700 px-4 py-2 rounded-lg font-bold text-white">
          Created
        </button>
      </div>

      <div className="w-80 p-4 rounded-lg flex justify-between bg-green-300 transition-all duration-200 ease-in-out hover:scale-110">
        <div>
          <p className="font-bold">Driver: John Smith</p>
          <p>Seats: 1 out of 4</p>
        </div>
        <button className="bg-white px-4 py-2 rounded-lg text-black font-bold">
          Join
        </button>
      </div>
    </div>
  );
}

export default Oppertunities;
