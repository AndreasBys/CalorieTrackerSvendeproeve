// imports mongoose to use to create schema
import mongoose from 'mongoose'

// imports models to use in schema
import Food from './food.js'
import Dish from './dish.js'

const macroLogSchema = new mongoose.Schema({
    date: {
        type: Date,
        required: true,
        default: Date.now
    },
    weight: {
        type: Number,
        required: true
    },
    user: {
        type: mongoose.Schema.Types.ObjectId,
        ref: "User"
    },
    food: {
        type: mongoose.Schema.Types.ObjectId,
        ref: "Food",
        required: false
    },
    dish: {
        type: mongoose.Schema.Types.ObjectId,
        ref: "Dish",
        required: false
    }
})

macroLogSchema.pre('save', async function (next) {
    // Sikrer at enten food eller dish er sat
    if (!this.food && !this.dish) {
        return next(new Error('Either food or dish is required.'));
    }

    try {
        // Tjek om food findes, hvis food er sat
        if (this.food) {
            const foodItem = await Food.findById(this.food);
            if (!foodItem) {
                return next(new Error('Food item not found.'));
            }
        }

        // Tjek om dish findes, hvis dish er sat
        if (this.dish) {
            const dishItem = await Dish.findById(this.dish);
            if (!dishItem) {
                return next(new Error('Dish item not found.'));
            }
        }

        next(); // Hvis alt er OK, fortsæt med at gemme
    } catch (err) {
        next(err); // Send fejl videre
    }
});


// exports scheme as model to mongoose database and controllers
const MacroLog = mongoose.model('MacroLog', macroLogSchema)
export default MacroLog;