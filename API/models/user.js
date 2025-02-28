// imports mongoose to use to create schema
import mongoose from "mongoose";

// imports bcrypt to encrypt password
import bcrypt from 'bcrypt';

const userSchema = new mongoose.Schema({
    username: {
        type: String,
        required: [true, 'Username is required'], // username is required
        minLength: 3, // sets minimun username lenght to 3 characters
        unique: true, // makes sure username is unique
        validate: {
            // validate to make sure username is containing correct characters
            validator: function (value) {
                const regex = /^[a-zA-ZæøåÆØÅ0-9]*$/;
                return regex.test(value);
            },
            message: 'Username can only contain letters and numbers'
        }
    },
    email: {
        type: String,
        required: [true, 'Email is required'], // email is required
        minLength: 6, // makes sure email is the right minimun lenght ('x@x.xx)
        unique: true, // makes sure email is unique
        validate: {
            // validate to make sure email is containing correct characters
            validator: function (value) {
                const regex = /^(([^<>()[\]\\.,;:\s@']+(\.[^<>()[\]\\.,;:\s@']+)*)|.('.+'))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
                return regex.test(value);
            },
            message: 'Provided email is not valid'
        }
    },
    password: {
        type: String,
        required: [true, 'Password is required']
    },
    birthdate: {
        type: Date,
        required: false
    },
    weight: {
        type: Number,
        required: false
    },
    height: {
        type: Number,
        required: false
    },
    gender: {
        type: String,
        required: false
    },
    admin: {
        type: Boolean,
        required: true, // true because user either is admin or not
        default: false // defaults to false as every new user cannot be admin right away
    }
});

userSchema.pre('save', async function (next) {
    const user = this;
    if (!user.isModified('password')) return next();

    try {
        const salt = await bcrypt.genSalt();
        user.password = await bcrypt.hash(user.password, salt);
        next();
    } catch (err) {
        return next(err);
    }
});

userSchema.methods.comparePassword = async function (password) {
    return bcrypt.compare(password, this.password)
}

// exports scheme as model to mongoose database and controllers
const User = mongoose.model('User', userSchema)
export default User;
