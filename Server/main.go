package main

import (
	"github.com/gorilla/mux"
	"net/http"
	"log"
	"encoding/json"
)

type Person struct {
	ID string `json:"id,omitempty"`
	Name string `json:"name,omitempty"`
}

func main() {
	router := mux.NewRouter();
	router.HandleFunc("/", Test).Methods("POST");
	router.HandleFunc("/Test", Test).Methods("POST");
	log.Fatal(http.ListenAndServe(":2345", router));
}

func Test(w http.ResponseWriter, r *http.Request) {
	var people []Person;
	var newPeople []Person;
	json.NewDecoder(r.Body).Decode(&people);
	newPeople = make([]Person, len(people));
	i := 0;
	for _, person := range people {
		newPeople[i] = person;
		newPeople[i].Name = "Copycat!";
		i++;
	}
	json.NewEncoder(w).Encode(newPeople);
}