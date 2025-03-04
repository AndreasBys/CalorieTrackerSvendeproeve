// imports mongoose to use to create schema
import mongoose from 'mongoose'

const foodSchema = new mongoose.Schema({
    barcode: {
        type: String,
        required: false
    },
    name: {
        type: String,
        required: false
    },
    godkendt: {
        type: Boolean,
        required: false
    },
    calories: {
        type: Number,
        required: false
    },
    carbonhydrates: {
        type: Number,
        required: false
    },
    protein: {
        type: Number,
        required: false
    },
    fat: {
        type: Number,
        required: false
    },
    user: {
        type: mongoose.Schema.Types.ObjectId,
        ref: 'User',
        required: false
    }
})

// exports scheme as model to mongoose database and controllers
const Food = mongoose.model('Food', foodSchema)
export default Food;