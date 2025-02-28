// imports mongoose to use to create schema
import mongoose from 'mongoose'

const dishSchema = new mongoose.Schema({
    
})

// exports scheme as model to mongoose database and controllers
export const Dish = mongoose.model('Dish', dishSchema)