var handleSearch = function(event) {
    event.preventDefault();
    // Get the search terms from the input field
    var searchPhrase = event.target.elements['search'].value;
    // Tokenize the search terms and remove empty spaces
    var tokens = searchPhrase
                  .toLowerCase()
                  .split(' ')
                  .filter(function(token){
                    return token.trim() !== '';
                  });
   if(tokens.length) {
    //  Create a regular expression of all the search terms
    var searchPhraseRegex = new RegExp(tokens.join('|'), 'gim');
    var filteredList = carpools.filter(function(carpool){
      // Create a string of all object values
      var carpoolString = '';
      for(var key in carpool) {
        if(book.hasOwnProperty(key) && carpool[key] !== '') {
            carpoolString += carpool[key].toString().toLowerCase().trim() + ' ';
        }
      }
      // Return carpool objects where a match with the search regex if found
      return carpoolString.match(searchPhraseRegex);
    });
    // Render the search results
    render(filteredList);
   }
  };