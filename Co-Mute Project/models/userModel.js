const mongoose = require('mongoose');
const bcrypt = require('bcrypt');
const {isEmail} = require('validator');

const UserSchema = new mongoose.Schema({
    name: {
        type: String,
        required: true
    },
    surname: {
        type: String,
        requird: true
    },
    cell_number: {
        type: Number,
        required: false
    },
    email: {
        type: String,
        required: [true, 'Please enter an email'],
        unique: true,
        lowercase: true,
        validate: [isEmail, 'Please enter a valid email']
    },
    password: {
        type: String,
        required: [true, 'Please enter a password'],
        minLength: [6, 'Minimum password length is 6 characters']
    },
    joined_carPools: {
        type: Array,
        required: false
    },
    createdPools: {
        type: Number,
        required: false
    },
    prevJoined: {
        type: Array,
        required: false
    }
}, {timestamps: true})


// hashing the user password for more security
UserSchema.pre('save', async function(next) {
    const salt = await bcrypt.genSalt();
    this.password = await bcrypt.hash(this.password, salt);
    next();
})

// logging the user in
UserSchema.statics.login = async function(email, password) {
    const user = await this.findOne({email});
    if (user) {
        const passwordMatch = await bcrypt.compare(password, user.password);
        if (passwordMatch) {
            return user
        }
        throw Error('Invalid password!')
    }
    throw Error('User with that email not found')
}

// to hash any password
UserSchema.statics.hashPassword = async function(newPassword) {
    const salt = await bcrypt.genSalt();
    new_pass = await bcrypt.hash(newPassword, salt);
    return new_pass;
}

const UserModel = mongoose.model('User', UserSchema);

UserModel.createIndexes();

module.exports = UserModel;