// importing models
import Dish from "../models/dish.js"
import FoodInDish from "../models/foodInDish.js"

// exporting get all method - gets all dishes
export const getAllUserDishes = async (req, res) => {
    try {
        // Find alle retter tilhørende brugeren
        const dishes = await Dish.find({ user: req.user.id });

        // Hent foods til hver ret
        // Promise.all fungerer lidt som en forEach, men kan håndtere async. 
        // Map laver et nyt array fra dishes, hvor hver ret bliver modificeret til at inkludere foods
        const dishesWithFoods = await Promise.all(
            dishes.map(async (dish) => {
                const foods = await FoodInDish.find({ dish: dish._id })
                    .populate('food')
                    .select('food weight');

                return { ...dish.toObject(), foods };
            })
        );

        // Returnér retter inklusiv deres foods
        res.status(200).json({ dishes: dishesWithFoods });
    } catch (error) {

        res.status(500).json({ msg: "unable to get dishes" });
    }
};

// exporting get method - gets specific dish through id
export const getDish = async (req, res) => {
    try {
        // finder retten
        const dish = await Dish.findById(req.params.id);

        // finder ingredienserne til retten
        const foods = await FoodInDish.find({ dish: dish.id })
            .populate('food')
            .select('food weight');

        // returnerer retten og tilhørende foods
        res.status(200).json({ dish, foods });
    } catch (error) {
        // håndterer fejl
        res.status(500).json({ msg: "unable to get dish" });
    }
};

// exporting create method - creates new dish
export const createDish = async (req, res) => {
    try {
        req.body.user = req.user.id;

        // Opretter og gemmer retten
        const savedDish = await new Dish(req.body).save();

        // Opretter FoodInDish entries
        const foodInDishPromises = Object.keys(req.body.foods).map(key => {
            return new FoodInDish({
                dish: savedDish._id,
                food: req.body.foods[key].id,
                weight: req.body.foods[key].weight
            }).save();
        });

        // Venter på at alle FoodInDish-entries bliver gemt
        await Promise.all(foodInDishPromises);

        // Finder og populater variablen til retur
        const foodInDishEntries = await FoodInDish.find({ dish: savedDish._id })
            .populate('food')
            .select('food weight');

        // Sender respons med både retten og de tilhørende FoodInDish-entries
        res.status(201).json({ 
            msg: 'dish saved', 
            savedDish, 
            Foods: foodInDishEntries 
        });

    } catch (error) {
        res.status(500).json({ msg: 'unable to save new dish' });
    }
};

// exporting delete method - deletes a dish through id
export const deleteDish = async (req, res) => {
    try {
        // gets id from params
        const id = req.params.id;

        // First, delete all FoodInDish entries related to this dish
        await FoodInDish.deleteMany({ dish: id });

        // Then, delete the dish itself
        const deletedDish = await Dish.findByIdAndDelete(id);

        if (!deletedDish) {
            return res.status(404).json({ msg: "Dish not found" });
        }

        // returns the deleted dish
        res.status(200).json({
            msg: "The following dish has been deleted",
            dish: deletedDish
        });

    } catch (error) {
        // logs and returns status 500 if error
        res.status(500).json({ msg: "Unable to delete dish" });
    }
};

// opdatere ret gennem
export const updateDish = async (req, res) => {
    try {
        // gets id from params
        const id = req.params.id;

        // sets new dish with body  
        const dishUpdate = req.body;

        // updates the dish in database
        const updatedDish = await Dish.findOneAndUpdate({ _id: id }, dishUpdate, { new: true });

        // return the updated dish
        res.status(200).json({ 
            msg: "dish updated", 
            dish: updatedDish 
        });

    } catch (error) {
        // håndterer fejl
        res.status(500).json({ msg: "unable to update dish" });
    }
};