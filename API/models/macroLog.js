// imports mongoose to use to create schema
import mongoose from 'mongoose'

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
        required: true
    }
})

macroLogSchema.pre('save', async function (next) {
    if (!this.isModified('food')) return next(); // Hvis food ikke ændres, gå videre

    try {
        const foodItem = await Food.findById(this.food);
        if (!foodItem) {
            return next(new Error('Food item not found'));
        }
        next(); // Fortsæt hvis food findes
    } catch (err) {
        next(err); // Send fejl videre
    }
});

// exports scheme as model to mongoose database and controllers
const MacroLog = mongoose.model('MacroLog', macroLogSchema)
export default MacroLog;