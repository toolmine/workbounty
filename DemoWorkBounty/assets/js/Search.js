(function () {
    var SearchViewModel = function () {
        var self = this;

        self.Persons = ko.observableArray([]); 

        self.FilterValue = ko.observable(); 

        self.ErrorMessage = ko.observable("");

        var PersonInfo = {
            Email: "",
            FirstName: "",
            LastName: "",
           
        };

        self.Properties = ko.observableArray([]); 

       
        loadProperties();

    
        function loadProperties() {
            for (prop in PersonInfo) {
                if (typeof PersonInfo[prop] !== 'function') {
                    self.Properties.push(prop);
                }
            }
        }

        self.Property = ko.observable("");//For the Property Name selected
        self.SelectedProperty = ko.observable();

        var searchFlag = 0;
        //The Function for Selection of the Property for Search Criteria
        self.SelectedProperty.subscribe(function (val) {
            if (val !== 'undefined') {
                self.Property(val);
                searchFlag = 1;
            }
        });




        loadPersons(); //Function Call

        //Function to Get All Persons
        function loadPersons() {
            $.ajax({
                url: "/Persons",
                type: "GET"
            }).done(function (resp) {
                self.Persons(resp);
            }).error(function (err) {
                self.ErrorMessage("Error " + err.status);
            });
        }


        //Make an ajax call to WEB API
        //And Put Data in Persons observablearray

        self.FilterValue.subscribe(function (val) {
            if (val !== 'undefined' || val !== '') {
                if (searchFlag === 1) {
                    var url = "/Persons/" + self.Property() + "/" + val;
                    $.ajax({
                        url: url,
                        type: "GET"
                    }).done(function (resp) {
                        self.Persons(resp);
                    }).error(function (err) {
                        self.ErrorMessage("Error " + err.status);
                    });
                }
            }
            if (val === '') {
                loadPersons();
            }
        });

    };

    ko.applyBindings(new SearchViewModel());
})();