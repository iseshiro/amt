clear all;
global inv_leng sig_leng mic_ch sp_ch;
set_parameter
sig_sp = zeros(sig_leng + inv_leng - 1 , sp_ch);
Fs = 48000;
for mm = 1:mic_ch
    sig_mic = 0.5 * rand(sig_leng,1) - 0.25;
    audiowrite(sprintf('sig_mic_%d.wav', mm), single(sig_mic), Fs,'BitsPerSample',32);
    for ss = 1:sp_ch
        inv = 0.5 * rand(inv_leng,1) - 0.25;
        %% audiowrite(sprintf('inv_s%d_m%d.wav', ss, mm), single(inv), Fs,'BitsPerSample',32);
        fout = fopen(sprintf('inv_s%d_m%d.double', ss, mm),'wb');
        fwrite(fout, inv, 'double');
        fclose(fout);
        xx = conv(sig_mic, inv);
        sig_sp(:, ss) = sig_sp(:, ss) + xx;
    end
end
for ss = 1:sp_ch
    audiowrite(sprintf('sig_sp_%d.wav', ss), single(sig_sp(:,ss)), Fs,'BitsPerSample',32);
end