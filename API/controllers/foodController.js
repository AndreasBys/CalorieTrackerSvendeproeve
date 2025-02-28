// importing food model
import Food from "../models/food.js"

// exporting get all method - gets all foods
export const getFoods = async (req, res) => {
    try {
        Food.find()
        .then((foods) => {
            // returns status 200 and all foods
            res.status(200).json({foods: foods})
        })
    } catch (error) {
        // return status 500 error if failed
        console.log(error)
        res.status(500).json({msg: "unable to get foods"})
    }
}

// exporting get method - gets specific food through id
export const getFood = async (req, res) => {
    try {
        const id = req.params.id
        Food.findById(id)
        .then((foods) => {
            // returns status 200 and food that matches id
            res.status(200).json({food: foods})
        })
    } catch (error) {
        // logs and returns status 500 error if failed or no foods found
        console.log(error)
        res.status(500).json({msg: "unable to get food"})
    }
};

// exporting search method - gets food that match certain criterias
export const search = async (req, res) => {
    try {
        // creates search term from the request query
        const searchTerm = req.query.searchTerm
        
        // creates regex for search
        const searchRegex = new RegExp(searchTerm, "i")
        
        // specifies criterias to search for
        await Food.find({
            $or : [
                {barcode: searchRegex},
                {name: searchRegex},
                {calories: searchRegex},
                {carbonhydrates: searchRegex},
                {protein: searchRegex},
                {fat: searchRegex}
            ]
        })
        .then((foods) => {
            if(foods.lenght){
                // logs and returns the foods that match
                console.log(foods)
                res.status(200).json({foods: foods})
            }
            else{
                // returns nothing if no foods match
                res.status(200).json({foods: [], msg: "no foods found"})
            }
        })
    } catch (error) {
        // logs and returns status 500 if error
        console.log(error)
        res.status(500).json({msg: "unable to get food"})
    }
};

// exporting create method - creates new food
export const createFood = async (req, res) => {
    try {
        // creates a food from food body
        const food = new Food(req.body)

        // saves the new food to database
        await food.save()
        .then((savedFoods) => {
            // logs and returns the created food
            console.log(savedFoods)
            res.status(201).json({msg: 'food saved', food})
        })
    } catch (error) {
        // logs and returns status 500 if error -> food not created
        console.log(error)
        res.status(500).json({msg: 'unable to save new food'})
    }
    
}

// exporting delete method - deletes a food through id
export const deleteFood = async (req, res) => {
    try {
        // gets id from params
        const id = req.params.id

        // deletes the food from database
        await Food.findByIdAndDelete(id)
        .then((foods) => {
            // returns the deleted food
            res.status(200).json({msg: "Following food has been deleted", 
                food: foods})
        })
    } catch (error) {
        // logs and returns status 500 if error 
        // food not deleted or couldn't be found
        console.log(error)
        res.status(500).json({msg: "unable to delete food"})
    }
}

// exporting update method - updates a food through id
export const updateFood = async (req, res) => {
    try {
        // gets id from params
        const id = req.params.id

        // sets new food with body  
        const updatedFood = req.body

        // updates the food in database
        await Food.findOneAndUpdate({_id: id}, updatedFood, {new: true})
        .then((updatedFood) => {
            // logs and return the updated food
            console.log(updatedFood)
            res.status(200).json({msg: "food updated", food: updatedFood})
        })
    } catch (error) {
        // logs and returns status 500 if error 
        // food couldn't be found or couldn't be updated
        console.log(error)
        res.status(500).json({msg: "unable to update food"})
    }
}