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
    calorieGoal: {
        type: Number,
        required: false
    },
    carbonhydratesGoal: {
        type: Number,
        required: false
    },
    proteinGoal: {
        type: Number,
        required: false
    },
    fatGoal: {
        type: Number,
        required: false
    },
    margin: {
        type:Number,
        required: false
    },
    user: {
        type: mongoose.Schema.Types.ObjectId,
        ref: "User"
    }
})

// exports scheme as model to controllers
const Goal = mongoose.model('Goal', goalSchema);
export default Goal;



