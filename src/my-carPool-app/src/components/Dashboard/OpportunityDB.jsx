const OpportunityDB = [
  {
    id: 1,
    driver: "Alice Johnson",
    seats: "2 out of 4",
    created: "1 February 2022",
    type: "Created",
  },
  {
    id: 2,
    driver: "Bob Smith",
    seats: "4 out of 4",
    created: "2 February 2022",
    type: "Full",
  },
  {
    id: 3,
    driver: "Charlie Brown",
    seats: "2 out of 4",
    created: "3 February 2022",
    type: "Joined",
  },
  {
    id: 4,
    driver: "David Lee",
    seats: "3 out of 4",
    created: "4 February 2022",
    type: "Created",
  },
  {
    id: 5,
    driver: "Emily Davis",
    seats: "4 out of 4",
    created: "5 February 2022",
    type: "Full",
  },
  {
    id: 6,
    driver: "Frank Martinez",
    seats: "2 out of 4",
    created: "6 February 2022",
    type: "Joined",
  },
  {
    id: 7,
    driver: "Grace Kim",
    seats: "1 out of 4",
    created: "7 February 2022",
    type: "Created",
  },
  {
    id: 8,
    driver: "Henry Lee",
    seats: "4 out of 4",
    created: "8 February 2022",
    type: "Full",
  },
  {
    id: 9,
    driver: "Isabelle Smith",
    seats: "2 out of 4",
    created: "9 February 2022",
    type: "Joined",
  },
  {
    id: 10,
    driver: "John Doe",
    seats: "3 out of 4",
    created: "10 February 2022",
    type: "Created",
  },
];

const Opportunity = ({ driver, seats, created, type }) => {
  return (
    <div className="w-80 p-4 rounded-lg mb-4 flex justify-between bg-green-300 transition-all duration-200 ease-in-out hover:scale-110">
      <div>
        <p className="font-bold">Driver: {driver}</p>
        <p>Seats: {seats}</p>
        <p>Created: {created}</p>
      </div>
      <button className={`px-4 py-2 rounded-lg font-bold text-white ${type === 'Created' ? 'bg-gray-500' : 'bg-green-700'}`}>
        {type}
      </button>
    </div>
  );
};

const OpportunityList = () => {
  return (
    <div>
      {OpportunityDB.slice(0, 4).map((item, index) => (
        <Opportunity key={index} {...item} />
      ))}
    </div>
  );
};

export default OpportunityList;
