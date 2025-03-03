// importing dish model
import Dish from "../models/dish.js"
import Food from "../models/food.js"
import FoodInDish from "../models/foodInDish.js"

// exporting get all method - gets all dishes
export const getAllUserDishes = async (req, res) => {
    try {
        const userId = req.user.id
        req.body.user = userId;

        Dish.findById({ user: userId })
            .then((dishes) => {
                // returns status 200 and all dishes
                res.status(200).json({ dishes: dishes })
            })
    } catch (error) {
        // return status 500 error if failed
        console.log(error)
        res.status(500).json({ msg: "unable to get dishes" })
    }
}

// exporting get method - gets specific dish through id
export const getDish = async (req, res) => {
    try {
        const id = req.params.id
        Dish.findById(id)
            .then((dishes) => {
                // returns status 200 and dish that matches id
                res.status(200).json({ dish: dishes })
            })
    } catch (error) {
        // logs and returns status 500 error if failed or no dishes found
        console.log(error)
        res.status(500).json({ msg: "unable to get dish" })
    }
};

// exporting search method - gets dish that match certain criterias
export const search = async (req, res) => {
    try {
        // creates search term from the request query
        const searchTerm = req.query.searchTerm

        // creates regex for search
        const searchRegex = new RegExp(searchTerm, "i")

        // specifies criterias to search for
        await Dish.find({
            name: searchRegex
        })
            .then((dishes) => {
                if (dishes.lenght) {
                    // logs and returns the dishes that match
                    console.log(dishes)
                    res.status(200).json({ dishes: dishes })
                }
                else {
                    // returns nothing if no dishes match
                    res.status(200).json({ dishes: [], msg: "no dishes found" })
                }
            })
    } catch (error) {
        // logs and returns status 500 if error
        console.log(error)
        res.status(500).json({ msg: "unable to get dish" })
    }
};

// exporting create method - creates new dish
export const createDish = async (req, res) => {
    // saves the new dish to database
    try {
        await new Dish(req.body).save()
            .then(async (savedDish) => {
                Object.keys(req.body.foods).forEach(async key => {
                    await new FoodInDish({
                        dish: savedDish._id,
                        food: req.body.foods[key].id,
                        weight: req.body.foods[key].weight
                    }).save()
                });
                // logs and returns the created dish
                console.log(savedDish)
                res.status(201).json({ msg: 'dish saved', savedDish })
            })
    } catch (error) {
        console.log(error)
        res.status(500).json({ msg: 'unable to save new dish' })
    }

}

// exporting delete method - deletes a dish through id
export const deleteDish = async (req, res) => {
    try {
        // gets id from params
        const id = req.params.id

        // deletes the dish from database
        await Dish.findByIdAndDelete(id)
            .then((dishes) => {
                // returns the deleted dish
                res.status(200).json({
                    msg: "Following dish has been deleted",
                    dish: dishes
                })
            })
    } catch (error) {
        // logs and returns status 500 if error 
        // dish not deleted or couldn't be found
        console.log(error)
        res.status(500).json({ msg: "unable to delete dish" })
    }
}

// exporting update method - updates a dish through id
export const updateDish = async (req, res) => {
    try {
        // gets id from params
        const id = req.params.id

        // sets new dish with body  
        const updatedDish = req.body

        // updates the dish in database
        await Dish.findOneAndUpdate({ _id: id }, updatedDish, { new: true })
            .then((updatedDish) => {
                // logs and return the updated dish
                console.log(updatedDish)
                res.status(200).json({ msg: "dish updated", dish: updatedDish })
            })
    } catch (error) {
        // logs and returns status 500 if error 
        // dish couldn't be found or couldn't be updated
        console.log(error)
        res.status(500).json({ msg: "unable to update dish" })
    }
}