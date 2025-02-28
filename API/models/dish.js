// imports mongoose to use to create schema
import mongoose from 'mongoose'

const dishSchema = new mongoose.Schema({
    name: {
        type: String,
        required: false
    },
    user: {
            type: mongoose.Schema.Types.ObjectId,
            ref: "User"
        }
})

// exports scheme as model to mongoose database and controllers
const Dish = mongoose.model('Dish', foodSchema)
export default Dish;