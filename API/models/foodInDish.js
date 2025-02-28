// imports mongoose to use to create schema
import mongoose from 'mongoose'

const foodInDishSchema = new mongoose.Schema({
    
})

// exports scheme as model to mongoose database and controllers
const FoodInDish = mongoose.model('FoodInDish', foodInDishSchema);
export default FoodInDish;