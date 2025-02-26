// imports mongoose to use to create schema
import mongoose from 'mongoose'

const macroGoalSchema = new mongoose.Schema({
    
})

// exports scheme as model to mongoose database and controllers
export const MacroGoal = mongoose.model('MacroGoal', macroGoalSchema)