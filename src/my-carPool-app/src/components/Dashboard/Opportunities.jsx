import OpportunityList from "./OpportunityDB";

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
      <OpportunityList />
    </div>
  );
}

export default Oppertunities;
