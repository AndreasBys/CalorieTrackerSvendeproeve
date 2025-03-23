// importing food model
import Food from "../models/food.js"

// gets all foods
export const getFoods = async (req, res) => {
    try {
        let filter = {}

        // sets filter to only get approved foods or ones made by user if user is not admin
        if (req.user.admin == false) {
            filter = {
                $or: [
                    { approved: true },
                    { user: req.user.id }
                ]
            }
        }

        // gets all foods with filter
        const foods = await Food.find(filter);

        // returns the foods
        res.status(200).json({ foods: foods })

    } catch (error) {
        // return status 500 error if failed
        res.status(500).json({ msg: "unable to get foods" })
    }
}

// gets specific food through barcode
export const getFood = async (req, res) => {
    try {
        // finds food by barcode
        const food = await Food.findOne({barcode: req.params.barcode})

        // checks if user is admin or user who created the food
        if (!(food.user == req.user.id || req.user.admin)) {
            return res.status(401).json({ msg: "no access" })
        }

        // returns the food
        res.status(200).json({ food })

    } catch (error) {
        // returns status 500 error if failed or no foods found
        res.status(500).json({ msg: "unable to get food" })
    }
};

// exporting search method - gets food that match certain criterias
export const search = async (req, res) => {
    try {
        // creates search term from the request query
        const searchTerm = req.query.searchTerm

        // creates regex for search
        const searchRegex = new RegExp(searchTerm, "i")

        let filter = {
            $or: [
                { barcode: searchRegex },
                { name: searchRegex }
            ]
        }

        // sets filter to only get approved foods or ones made by user if user is not admin
        if (req.user.admin == false) {
            filter = {
                $and: [
                    { user: req.user.id },
                    {
                        $or: [
                            { barcode: searchRegex },
                            { name: searchRegex }
                        ]
                    }
                ]
            };
        }

        // searches for foods with the filter
        const foods = await Food.find(filter)

        // returns the foods
        res.status(200).json({ foods })

    } catch (error) {
        // returns status 500 if error
        res.status(500).json({ msg: "unable to get food" })
    }
};

// exporting create method - creates new food
export const createFood = async (req, res) => {
    try {
        // makes sure food gets user id from the user making the request
        req.body.user = req.user.id

        // makes approved false if user is not admin
        if (req.user.admin == false) {
            req.body.approved = false
        }

        // saves the new food to database
        const food = await new Food(req.body).save()

        // logs and returns the created food
        res.status(201).json({ food })

    } catch (error) {
        // returns status 500 if error -> food not created
        res.status(500).json({ msg: 'unable to save new food' })
    }

}

// exporting delete method - deletes a food through id
export const deleteFood = async (req, res) => {
    try {
        // gets id from params
        const id = req.params.id

        // finds food
        const food = Food.findById(id)

        // throws error if food is not found
        if (!food) throw new Error("Food not found");

        // 401 response if user is not admin or user who created the food
        if (!(req.user.admin || req.user.id == food.user)) {
            return res.status(401).json({ message: 'No access' });
        }

        const deletedFood = await Food.findByIdAndDelete(id);

        // returns the deleted food
        res.status(200).json({
            msg: "Following food has been deleted",
            food: deletedFood
        })
    } catch (error) {
        // returns status 500 if error 
        res.status(500).json({ msg: "unable to delete food" })
    }
}

// exporting update method - updates a food through id
export const updateFood = async (req, res) => {
    try {
        // gets id from params
        const id = req.params.id

        // updates the food in database
        const updatedFood = await Food.findOneAndUpdate({ _id: id }, req.body, { new: true })

        // return the updated food
        res.status(200).json({ msg: "food updated", food: updatedFood })
    } catch (error) {
        // returns status 500 if error 
        res.status(500).json({ msg: "unable to update food ", error })
    }
}