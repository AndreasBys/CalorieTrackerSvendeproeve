// imports mongoose to use to create schema
import mongoose from 'mongoose'

const goalSchema = new mongoose.Schema({
    startDate: {
        type: Date,
        required: true
    },
    endDate: {
        type: Date,
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
    proteins: {
        type: Number,
        required: false
    },
    fats: {
        type: Number,
        required: false
    },
    margin: {
        type:Number,
        required: false,
        default: 5
    },
    user: {
        type: mongoose.Schema.Types.ObjectId,
        ref: "User"
    }
})

// exports scheme as model to controllers
const MacroGoal = mongoose.model('MacroGoal', goalSchema);
export default MacroGoal;