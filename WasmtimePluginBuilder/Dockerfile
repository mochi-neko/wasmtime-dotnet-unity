FROM --platform=linux/x86_64 rust:latest

RUN apt-get update && \
    apt-get install -y \
        git \
        unzip \
        qemu-user \
        cmake

# Install Android NDK
RUN curl https://dl.google.com/android/repository/android-ndk-r25c-linux.zip?hl=ja -o ndk.zip
RUN unzip ndk.zip
ENV ANDROID_NDK_HOME $PWD/android-ndk-r25c
ENV PATH $PATH:${ANDROID_NDK_HOME}/toolchains/llvm/prebuilt/linux-x86_64/bin

# Setup Wasmtime
RUN git clone https://github.com/bytecodealliance/wasmtime.git
WORKDIR /wasmtime
RUN git checkout v5.0.0
RUN git submodule update --init --recursive
WORKDIR /wasmtime/crates/c-api

COPY cargo-config.toml ~/.cargo/config

RUN mkdir -p ~/Builds

RUN rustup target add aarch64-linux-android
#RUN cargo build --target aarch64-linux-android --release
RUN mkdir build
WORKDIR /wasmtime/crates/c-api/build
RUN cmake ..
RUN make
RUN cp -R ../build ~/Builds/aarch64-linux-android

#RUN rustup target add armv7-linux-androideabi
#RUN cargo build --target armv7-linux-androideabi --release
#RUN cp -R target/armv7-linux-androideabi/release/* /Builds/armv7-linux-androideabi/